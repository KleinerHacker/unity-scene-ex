# unity-scene-ex
Scene Management Extension to switch easier between scenes at runtime.

# install
Use this repository directly in Unity.

### Dependencies
* https://github.com/KleinerHacker/unity-extension
* https://github.com/KleinerHacker/unity-editor-ex
* https://github.com/KleinerHacker/unity-blending

### Open UPM
URL: https://package.openupm.com

Scope: org.pcsoft

# usage
Extends from class `SceneSystem` and (for editor) `SceneSystemEditor`, 
use class `SceneData` as base class for known scenes (as enumeration).
If you want to manipulate loaded scenes override `FindSceneData` method in `SceneSystem` class.

### Best Precis
Create a "master" scene within your implemented `SceneSystem` class, a `BlendingSystem` class and
its game objects. Always startup project from here now.
