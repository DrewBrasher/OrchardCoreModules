# Blazor Module
Everything you need to include Blazor components inside the pages, MVC views, and Template Views of your Orchard Core Module.

**NOTE: This currently only works with Themes that have Razor layouts like TheTheme. It does not work with Themes like the Blog or Agency Themes that use liquid layouts.**

## Usage
1. Add a refference to the `DrewBrasher.OrchardCore.Blazor` project to your Orchard Core Module.

2. Add `DrewBrasher.OrchardCore.Blazor` to the Dependencies of your Module's `Manifest.cs`` file.
   ```
	Dependencies = [ "DrewBrasher.OrchardCore.Blazor" ]
   ```
3. Add the tag helper to your `_ViewImports.cshtml` file:
   ```
	@addTagHelper *, DrewBrasher.OrchardCore.Blazor
   ```
4.  Add your component to a page or view like this:
	```
	<blazor-component type="typeof(DemoComponent)" render-mode="ServerPrerendered" />
	```
	If your theme uses a Zone other than `HeadMeta` for rendering meta data, you can specify the zone like this:
	```
	<blazor-component type="typeof(DemoComponent)" render-mode="ServerPrerendered" header-zone="YourHeadZoneHere" />
	```
## Related Solution Projects
### DrewBrasher.OrchardCore.Blazor
Everything you need to include Blazor components inside the pages, MVC views, and Template Views of your Orchard Core Module.

### OCRazorModuleDemo
This is an Orchard Core CMS Module to demo and test this project. It has an MVC View and a Widget that both use the DemoComponent.

### RazorDemo
This is a Razor Class Library with a DemoComponent to use for testing this project.

## Contributing
If you would like to help out with this module, here are the steps to test it out:
1. Clone this repository
2. Setup OC with the default theme (TheTheme)
3. Enable the OCRazorModuleDemo feature
4. Go to this url to test: `/OCRazorModuleDemo/Home/Index`

## Credits
Thank you to [ApacheTech](https://github.com/ApacheTech) for the `EnableBlazorComponentsTagHelper`!
