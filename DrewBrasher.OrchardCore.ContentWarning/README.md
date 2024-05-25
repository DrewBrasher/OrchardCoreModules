# Content Warning Module 
An Orchard Core CMS Module that allows you to put a content warning around some content so that users can decide if they want to see it or not.

There is a discussion about this module here: <https://github.com/OrchardCMS/OrchardCore/discussions/13643>

## NuGet Package
<https://www.nuget.org/packages/DrewBrasher.OrchardCore.ContentWarning/>

## Usage
### Shortcode
```
[content_warning 'warning-message'] some sensitive content [/content_warning]
```
or

```
[cw 'warning-message'] some sensitive content [/cw]
```

## Credits
The JavaScript and CSS are based on the <https://www.aaron-gustafson.com/notebook/considering-content-warnings-in-html/> blog post by [Aaron Gustafson](https://github.com/aarongustafson). 

I started this module in an Ochard Core Pair Programming by Lombiq session with [Zoltán Lehóczky](https://github.com/Piedone) that was live streamed here:
<https://www.youtube.com/watch?v=ECEbkLSOqLQ>

We will be doing another live session at 5pm UTC on May 28th, 2024. You can watch it here: <https://www.youtube.com/watch?v=SmvLpAfyEHI>
