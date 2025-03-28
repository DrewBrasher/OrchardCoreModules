# Blazor Module
This module is a work in progress. The goal is to have a base module that adds everything you need to include Blazor components inside the pages and views of your Orchard Core Module.

Ideally, I would like for the only thing needed in an OC module to use a Blazor component to be this:
1. Add this module as a dependency for your module
2.  Add your component to a page or view like this:
	```
	<component type="typeof(YourBlazorComponent)" render-mode="ServerPrerendered" />
	```
## Related Solution Projects
### DrewBrasher.OrchardCore.Blazor
This is the main project that I hope will have everything in it that is needed to add Blazor Components components inside the pages and views of your Orchard Core Modules.

### OCRazorModuleDemo
This is an Orchard Core CMS Module to demo and test this project. It has an MVC View that uses the DemoComponent.

### RazorDemo
This is a Razor Class Library with a DemoComponent to use for testing this project.

## Contributing
If you would like to help out with this module, here are the steps to test it out:
1. Clone this repository
2. Setup OC with the Agency Theme
3. Enable the OCRazorModuleDemo feature
4. Go to this url to test: `/OCRazorModuleDemo/Home/Index`