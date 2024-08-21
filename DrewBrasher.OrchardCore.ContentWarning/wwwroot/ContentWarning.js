const $content_warnings = document.querySelectorAll('[data-content-warning]');

class ContentWarningElement extends HTMLElement {

    constructor() {
        // Always call super first in constructor
        super();

        const $shadow = this.attachShadow({ mode: "closed" });

        this.$wrapper = document.createElement("span");
        this.$wrapper.setAttribute("class", "wrapper");
        $shadow.appendChild(this.$wrapper);

        this.$button = document.createElement('button');
        this.$button.addEventListener('click', this.reveal.bind(this));
        this.$wrapper.appendChild(this.$button);

        this.$original = null;

        let $style = document.createElement("style");
        $style.textContent = `
      .wrapper {
        background: #ebebeb;
        position: relative;
      }
      button {
        display: inline;
        text-align: center;
        cursor: pointer;
        align-items: center;
        justify-content: center;
        margin: 0;
      }
      button.block {
        display: block;
        position: absolute;
        inset: 0;
      }
    `;
        $shadow.appendChild($style);

    }

    swap($el) {
        this.$original = $el;
        this.$parent = $el.parentNode;
        this.original_styles = window.getComputedStyle($el);
        this.inheritStyles();
        this.positionButton();
        this.$button.innerText = `CW: ${$el.getAttribute("data-content-warning")}.`;
        this.hide($el);
    }

    inheritStyles() {
        // images and non-inline elements
        if (this.$original.nodeName.toLowerCase() == 'img' ||
            this.original_styles.display != "inline") {
            ["display", "width", "height", "margin"].map(property => {
                this.$wrapper.style[property] = this.original_styles[property];
            });
            if (this.original_styles.margin == "0px") {
                this.$wrapper.style["margin-block-start"] = window.getComputedStyle(this.$original.firstElementChild)["margin-block-start"];
                this.$wrapper.style["margin-block-end"] = window.getComputedStyle(this.$original.firstElementChild)["margin-block-end"];
            }
        }
        // everything else
        else {
            this.$button.style.blockSize = `${this.$original.offsetHeight}px`;
            this.$button.style.inlineSize = `${this.$original.offsetWidth}px`;
        }
    }

    positionButton() {
        if (this.$original.nodeName.toLowerCase() == 'img' ||
            this.original_styles.display != "inline") {
            this.$button.setAttribute("class", "button block");
        }
    }

    hide($el) {
        this.$parent.replaceChild(this, this.$original);
    }

    reveal() {
        this.$original.setAttribute("role", "alert");
        this.$parent.replaceChild(this.$original, this);
        focus(this.$original);
    }
}

customElements.define("content-warning", ContentWarningElement);

window.addEventListener("load", function () {
    $content_warnings.forEach($cw => {
        let $cwRehideButton = $cw.querySelector(".cw-rehide");
        $cwRehideButton.addEventListener('click', () => {
            let $cwElement = document.createElement('content-warning');
            $cwElement.swap($cw);
        });

        let $cwElement = document.createElement('content-warning');
        $cwElement.swap($cw); 
    });
}, false);