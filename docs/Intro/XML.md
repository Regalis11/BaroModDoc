# XML

Most of Barotrauma's content is defined in [XML files](https://en.wikipedia.org/wiki/XML). These code files may seem daunting if you've never used them before, but they're actually quite simple to understand even if you've never done any sort of programming.

The XML files can be edited with almost any text editor, even Notepad, but we recommend using a text editor that supports code highlighting and is able to point out mistakes in the file. For example, [Notepad++](https://notepad-plus-plus.org) is a free, easy to use software that supports XML code highlighting. Other good options are [Sublime Text](https://sublimetext.com) and [Visual Studio Code](https://code.visualstudio.com).

XML files consist of elements, delimited by an opening tag and a closing tag. In Barotrauma's case, an element could be for example an item. Such an element could be defined as follows:

```xml
<Item>
</Item>
```

As you might imagine, this alone isn't enough to fully define an item.

Elements can also have attributes, which usually give some additional information about the element. For example:

```xml
<Item identifier="alienwrench" name="Alien Wrench" variantof="wrench" scale="0.2">
</Item>
```

Elements can also have child elements. Building upon our last example, we could add a child element called `Sprite` which determines what the item looks like:

```xml
<Item identifier="alienwrench" name="Alien Wrench" variantof="wrench" scale="0.2">
  <Sprite texture="%ModDir%/alienwrench.png" sourcerect="0,0,256,112" depth="0.55" origin="0.5,0.1" scale="0.1" />
</Item>
```

Note that in this example, `Sprite` is a self-closing element. If no child elements need to be defined, you can choose to omit the closing tag by ending the opening tag with `/>` instead of simply `>`.

An encoding declaration may be optionally added at the top. It typically looks like this:
```xml
<?xml version="1.0" encoding="utf-8"?>
```

XML also supports adding comments. These are blobs of text that will be ignored by Barotrauma. They can be used to give hints about what the surrounding text is supposed to do.
```xml
<!-- This is what a comment looks like -->
<Item ...>
```

In Barotrauma, all XML files have the following constraints:
- There may be only one root element other than the encoding declaration. This is the first element that appears in the file; all subsequent elements must be children of this element.
- All of the attributes of an element must be unique.
