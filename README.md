# unity-scene-ex
Scene Management Extension to switch easier between scenes at runtime.

# install
Use this repository directly in Unity.

### Dependencies
* https://github.com/KleinerHacker/unity-editor-ex
* https://github.com/KleinerHacker/unity-blending
* https://github.com/KleinerHacker/unity-scene-base
* https://github.com/KleinerHacker/unity-common-ex
* https://github.com/KleinerHacker/unity-extension
* https://github.com/KleinerHacker/unity-scene-base
* https://github.com/marijnz/unity-toolbar-extender

### Open UPM
URL: https://package.openupm.com

Scopes:
* org.pcsoft
* com.marijnzwemmer

# usage
Setup your scene system in project settings:
![editor](https://github.com/KleinerHacker/unity-scene-ex/blob/7001dce3aa6fc384583b48094feb0683ebbc89ba/Docs/editor.png)

To load a new scene use always `SceneSystem` class.

:bangbang: Do not combine this with Unity World Extensions

### Parameter System
To use parameters you can create own parameter classes they must extends from `ParameterData`. Additional, to setup initila data for this parameters via Unity Editor you can create a simple `ScriptableObject` for this specific parameter data:

```CSharp
public interface IMyParameter 
{
  string Text { get; }
}

public sealed class MyParameterInitialData : ScriptableObject, IMyParameter
{
  [SerializeField]
  private string text;
  
  public string Text => text;
}

[ParameterInitialDataType(typeof(MyParameterInitialData))]
public sealed class MyParameterData : ParameterData, IMyParameter 
{
  private const string MyKey = "my.key";

  public string Text
  {
    get => Get<string>(MyKey);
    set => Add(MyKey, value);
  }
  
  public override void InitializeData(ScriptableObject initData) 
  {
    Text = ((MyParameterInitialData)initData).Text;
  }
}
```

To access the parameter simply use `SceneParameter`:
```CSharp
var parameter = SceneParameter.GetData<MyParameterData>();
```

If the parameter does not exists it is created and initialized by system and calling method `InitializeData` with scriptable object data setup in project settings.

### Events & Additional Scenes / Actions
To handle own actions you can add static callbacks for blending state changes and scene state changes:
```CSharp
[RuntimeOnSwitchScene(RuntimeOnSwitchSceneType.LoadScenes)]
public static void SwitchSceneLoad(RuntimeOnSwitchSceneArgs args)
{
    if (args.Identifier == MenuKey)
    {
        args.AdditionalScenes = new[] { SceneParameter.GetData<MenuParameter>().MenuScene };
    }
    
    args.Callback.Invoke();
}
```
So you can add additional scenes to load or unload.
```CSharp
[RuntimeOnBlendScene(RuntimeOnBlendSceneType.PostShowBlend)
public static void SwitchBlend(RuntimeOnBlendSceneArgs args)
{
  ...
  
  args.Callback.Invoke();
}
```
So you can do actions on specific blend actions.

__Please note: it is required to call the callback action to continue loading!__
