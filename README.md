# BlazorFill

WIP of NuGets with components and extension methods, mainly for convenient DOM interaction that is missing from Blazor
by default

### NuGets

#### BlazorFill.DOM

For DOM interaction, like adding CSS event listeners to elements or interacting with Window or Document.  
Should be independent of all the other NuGets.

#### BlazorFill.Template

CSS framework that all NuGets except for BlazorFill.DOM expects to be used.  
Added by writing this in `ìndex.html`:

```html

<link href="_content/BlazorFill.Template/template.css" rel="stylesheet"/>
```

#### BlazorFill.Elements

Elements that serves as a different approach from `<div class="{name}">` that is common from traditional CSS frameworks,
and instead allows you to instead just write `<Name>`.

#### BlazorFill.Components

Useful custom components, like fancy inputs, tables and typeahead.