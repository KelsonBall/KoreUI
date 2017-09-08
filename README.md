Graphics
# Kore UI

A graphics and UI framework for dotnet core, providing a minimal abstraction on OpenTK to allow for cross platform application development.

## Contributing

A lot of work needs done, including

 * Adding a set of base controls
 * Improving font rendering
 * Filling out the `Canvas` classes implementation of the `IRenderer2D` and `IRenderer3D` interfaces
 * Creating an MVVM framework supporting data binding and parsing UI structures from XML and JSON.

## Architecture

Rendering is encapsulated in the `Canvas` class, which implements an `IRenderer2D` interface and will implement an `IRenderer3D` interface in the future. 

The rendering interfaces provide a number of methods for drawing and the `Canvas` class implements them in OpenGL.

```C#
// IRenderer2D example methods
void Background(Color4 color);
void Triangle(PVector a, PVector b, PVector c);
void Rectangle(PVector position, PVector size);    
void Shape(PVector position, params PVector[] points);
...
void Image(PImage image, PVector position);
TextRenderResult Text(string text, PVector position);    
```

The `Application` class represents the root of the **visual tree**. Components are organized with a parent-child relationship, and parents are responsible for managing the layout and drawing of their children.

For example, an application containing 2 simple columns of buttons would look like this

* Application
    * Row
        * Column
            * Button 1
            * Button 2
            * Button 3
        * Column
            * Button 4
            * Button 5

And be described in code in a manner similar to this

```C#
new Application()
{
    new Row()
    {
        new Column()
        {
            new Button(button => button.Name = "Button 1"),
            new Button(button => button.Name = "Button 2"),
            new Button(button => button.Name = "Button 3"),
        },
        new Column()
        {
            new Button(button => button.Name = "Button 4"),
            new Button(button => button.Name = "Button 5"),
        }
    }
}.Show();
```

Coordinates are described using an (offset, scale) tuple for horizontal and vertical size and position. Offset is a distance in pixels, scale is a mulitplier of the parent controls size.

Here are some examples
```C#
new Application()
{
    // a square with half the height and width of the screen
    new UiControl(control => {
        // horizontal and then vertical (offset, scale) paris
        control.Size = ((0, 0.5), (0, 0.5));
        control.Background = Color4.Orange;
    }),

    // a square with 1/4th the height and width of the screen, aligned to the top right
    new UiControl(control => {
        control.Size = ((0, 0.25), (0, 0.25));
        control.Position = ((0, 0.75), (0, 0.75));
        control.Background = Color4.Green;
    }),

    // a square aligned to the bottom right with a 10 pixel margin    
    new UiControl(control => {
        control.Size = ((-20, 0.5), (-20, 0.5));
        control.Position = ((10, 0.5), (10, 0.5));
        control.Background = Color4.Blue;
    })
    {
        // a square centered in the bottom right square
        new UiControl(control => {
            control.Size = ((0, 0.5), (0, 0.5));
            control.Position = ((0, 0.25), (0, 0.25));
            control.Background = Color4.Cyan;
        })
    },
}.Show();
```

## Patterns 

The following patterns are used to enable use of modern C# features for quickly describing desired UI structures;

### Implicit Tuple Conversions for simple types

For simple numeric types provide an implicit conversion from the corresponding `System.ValueTuple` type.

Example
```C#
public class PVector
{
    public readonly double X;
    public readonly double Y:

    ...

    // allows tuples of doubles to be used as vectors
    public static implicit operator PVector ((double x, double y) v) => new PVector(v.x, v.y);
}

...

// Simplifies calls using PVector parameters from
canvas.Rectangle(new PVector(0, 0), new PVector(30, 30));
// to
canvas.Rectangle((0, 0), (30, 30));

```


### Defered Constructor Logic Pattern

To allow complex setup logic without breaking larger expressions, use an Action parameter to the constructor

Example

```C#
public class MyClass
{

    public MyClass()
    {
        // default ctor logic
    }

    public MyClass(Action<MyClass> setup) : this() => setup(this);
    // invokes default contstructor and then invokes setup action, allowing object initializer to run code during construction

}
 ```

 ### Utilize enumerables

 Classes that are collections should implement IEnumerable of their collected type. UiControl is an example of this

 ```C#
 public class  UiControl : IEnumerable<UiControl>
 {
    public IEnumerable<UiControl> Children 
    {
        get
        {
            // yields child controls
        }
    }

    // implements IEnumerable
    public IEnumerator<UiControl> GetEnumerator() => Children.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Children.GetEnumerator();
 }

 // can be used in Linq queries and foreach loops
 foreach (var child in myControl)
 {
     doSomething(child);
 }

 foreach (var child in myControl.Where(c => c.Name.StartsWith("Important")))
 {
     doSomethingOnImportantChild(child);
 }
 ```