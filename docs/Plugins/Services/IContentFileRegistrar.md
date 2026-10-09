# IContentFileRegistrar

```cs
public interface IContentFileRegistrar : IPluginService
```

Interface that allows registering custom XML content in your mod.

## Members

| Member                                             | Description                                                                                                                                                                                                    |
|----------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [`RegisterContentFile<T>()`](#registercontentfile) | Registers a content file type to be recognized by the game. The class should have the name "%NAME%File" where %NAME% is the name of the content type, and it should preferably inherit `GenericPrefabFile<T>`. |

## Methods

### RegisterContentFile

```cs
void RegisterContentFile<T>() where T : ContentFile
```

Registers a content file type to be recognized by the game. The class should have the name "%NAME%File" where %NAME% is the name of the content type, and it should preferably inherit `GenericPrefabFile<T>`.

| Type parameter | Description                                  |
|----------------|----------------------------------------------|
| `T`            | Implementation of `ContentFile` to register. |

**Example**

Barotrauma will recognize this example content type with the name "MyContent" due to the class name and alternatively as "FooBar" due to the attribute.<br/><br/> The content file implementation handles parsing the XML documents defined in filelist.xml and passing the found elements to the prefab class.

```csharp
[Barotrauma.AlternativeContentTypeNames("FooBar")]
public class MyContentFile : Barotrauma.GenericPrefabFile<MyPrefab>
{
    public MyContentFile(ContentPackage contentPackage, ContentPath path) : base(contentPackage, path) { }
    protected override bool MatchesSingular(Identifier identifier) => identifier == "MyPrefab";
    protected override bool MatchesPlural(Identifier identifier) => identifier == "MyPrefabs";
    protected override MyPrefab CreatePrefab(ContentXElement element) => new(this, element);
    protected override PrefabCollection<MyPrefab> Prefabs => MyPrefab.Prefabs;
}
```

<br/> An instance of the prefab class is created for each matching XML element found in the content file.

```csharp
public class MyPrefab : Barotrauma.Prefab
{
    public static readonly PrefabCollection<MyPrefab> Prefabs = new();
    public int MyProperty { get; set; }
    public MyPrefab(ContentFile file, ContentXElement element) : base(file, element.GetAttributeIdentifier("identifier", Identifier.Empty))
    {
        MyProperty = element.GetAttributeInt("myproperty", 0);
    }
    public override void Dispose() { }
}
```

<br/> Plugin init:

```csharp
IContentFile.RegisterContentFile<MyContentFile>();
```
