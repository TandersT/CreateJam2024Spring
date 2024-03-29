using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Directory = System.IO.Directory;
using System.Text;
using System.Reflection;
using System.Security.Cryptography;



/// <summary>
/// A class with various helper functions
/// </summary>
public static class Helper
{
    public static void SetMainAudioBus(float value)
    {
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), value);
    }
    #region helper-functions
    /// <summary>
    /// retuns the fullpath, depending on if its a debug build or release.
    /// </summary>
    /// <returns></returns>
    public static string GetExeFullPath()
    {
        return !OS.HasFeature("standalone") ? ProjectSettings.GlobalizePath("res://") : OS.GetExecutablePath();
    }
    /// <summary>
    /// Converts C sharp formatted code to GD formatted code (e.g. HelloWorld = hello_world)
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string CsharpToGDCode(string code)
    {
        string translatedCode = "";
        for (int i = 0; i < code.Length; i++)
        {
            char a = code[i];
            if (char.IsUpper(code[i]))
            {
                a = char.ToLower(code[i]);
                if (i != 0)
                {
                    translatedCode += "_";
                }
            }
            translatedCode += a;
        }
        return translatedCode;
    }


    public partial class EncapsulatedArea
    {
        public StaticBody2D holder;
        public CollisionShape2D top, bot, right, left;
    }

    static Random random = new Random();
    /// <summary>
    /// Loads an array of PackedScenes
    /// </summary>
    /// <param name="p"></param>
    /// <param name="s"></param>
    public static PackedScene[] GDLoad(string[] s)
    {
        var p = new PackedScene[s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            p[i] = (PackedScene)GD.Load(s[i]);
        }
        return p;
    }
    /// <summary>
    /// Checks if a value is within a given value
    /// </summary>
    /// <param name="value"></param>
    /// <param name="minimum"></param>
    /// <returns></returns>
    public static bool IsWithin(this float value, float minimum, float maximum)
    {
        return value >= minimum && value <= maximum;
    }
    /// <summary>
    /// Returns a random float between between min (inclusive) and max (exclusive)
    /// </summary>
    /// <param name="minimum"></param>
    /// <param name="maximum"></param>
    /// <returns></returns>
    public static float RandomFloat(float minimum, float maximum)
    {
        return (float)random.NextDouble() * (maximum - minimum) + minimum;
    }
    /// <summary>
    /// Returns a random int between min (inclusive) and max (exclusive)
    /// </summary>
    public static int RandomInt(int minimum, int maximum)
    {
        return minimum >= maximum ? 0 : random.Next(minimum, maximum);
    }

    /// <summary>
    /// Returns a -1 or 1 randomly
    /// </summary>
    public static int RandomPolarity()
    {
        return RandomInt(0, 2) == 0 ? -1 : 1;
    }
    /// <summary>
    /// Gives an interpolated float at a given delta
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static float Interpolate(float A, float B, float t)
    {
        return A + (B - A) * t;
    }
    /// <summary>
    /// Gives an interpolated vector2 at a given delta
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Vector2 Interpolate(Vector2 A, Vector2 B, float t)
    {
        return A + (B - A) * t;
    }
    /// <summary>
    /// Remaps a value from one range to another
    /// </summary>
    /// <param name="s"></param>
    /// <param name="a1"></param>
    /// <param name="a2"></param>
    /// <param name="b1"></param>
    /// <param name="b2"></param>
    /// <returns></returns>
    public static float Remap(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    static int numberOfTries = 1000;

    /// <summary>
    /// Given an enum type returns a random value of the enum type.
    /// </summary>
    /// <param name="excludedValues"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetRandomEnumValue<T>(params T[] excludedValues)
    {
        T[] values = (T[])Enum.GetValues(typeof(T));
        T generatedVal = values[RandomInt(0, values.Length)];
        if (excludedValues != null)
        {
            int generatedTries = 0;
            for (int i = 0; i < excludedValues.Length; i++)
            {
                if (EqualityComparer<T>.Default.Equals(excludedValues[i], generatedVal))
                {
                    generatedVal = values[RandomInt(0, values.Length)];
                    generatedTries++;
                    i = -1;//so when the loop finishes it gets set to 0
                }
                if (generatedTries > numberOfTries)
                {
                    GD.Print("Exceded the max number of tries.");
                    return generatedVal;
                }
            }
        }
        return generatedVal;
    }
    /// <summary>
    /// Returns the magntitude between 2 3D vectors
    /// </summary>
    public static float Magntitude(Vector3 a, Vector3 b)
    {
        var c = a - b;
        return Mathf.Abs(Mathf.Sqrt(Mathf.Pow(c.X, 2) + Mathf.Pow(c.Y, 2) + Mathf.Pow(c.Z, 2)));
    }
    /// <summary>
    /// Check if directory is empty
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool IsDirectoryEmpty(string path)
    {
        if (path != null)
        {
            CreateDirectoryIfNull(path);
            return !System.IO.Directory.EnumerateFileSystemEntries(path).Any();
        }
        else
        {
            return true;
        }
    }


    public static void CreateDirectoryIfNull(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }


    /// <summary>
    /// Searches all children and childrens childrens. All nodes are added to filledList
    /// </summary>
    /// <param name="filledList"></param>
    /// <param name="_Node"></param>
    public static void GetChildrenAndSubChildren<T>(ref List<T> filledList, Node _Node) where T : Node
    {
        foreach (Node child in _Node.GetChildren())
        {
            if (child is T desiredType)
            {
                filledList.Add(desiredType);
            }
            if (child.GetChildCount() > 0)
            {
                GetChildrenAndSubChildren(ref filledList, child);
            }
        }
    }

    /// <summary>
    /// Creates an autiostreamplayer, sets its stream and attaches it to a node 
    /// </summary>
    /// <param name="audio"></param>
    /// <param name="node"></param>
    /// <param name="moniker"></param>
    /// <returns></returns>
    public static AudioStreamPlayer AttachAudioStreamToNode(AudioStreamOggVorbis audio, Node node, string moniker = "DefaultAudio")
    {
        if (audio != null)
        {
            AudioStreamPlayer audioStream;
            if (node.GetNodeOrNull<AudioStreamPlayer>(moniker) == null)
            {
                audioStream = new AudioStreamPlayer();
                audioStream.Name = moniker;
                node.AddChild(audioStream);
                audioStream.Stream = audio;
            }
            else
            {
                audioStream = node.GetNode<AudioStreamPlayer>(moniker);
            }
            return audioStream;
        }
        return null;
    }
    /// <summary>
    /// Shuffles a list
    /// </summary>
    /// <param name="list"></param>
    /// <typeparam name="T"></typeparam>
    public static void ShuffleList<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    /// <summary>
    /// adds all values in an array of type int
    /// </summary>
    /// <param name="list"></param>
    /// <typeparam name="T"></typeparam>
    public static int SumIntList(this int[] array, int to)
    {
        int _value = 0;
        if (to > 0)
        {
            for (int i = 0; i <= to; i++)
            {
                _value += array[i];
            }
        }
        return _value;
    }

    public static void SwapValues<T>(this T[] source, long index1, long index2)
    {
        T temp = source[index1];
        source[index1] = source[index2];
        source[index2] = temp;
    }

    /// <summary>
    /// returns a randomly chosen AudioStream
    /// </summary>
    /// <param name="audioStreams"></param>
    /// <returns></returns>
    public static AudioStream RandomAudioStream(AudioStream[] audioStreams)
    {
        var t = RandomInt(0, audioStreams.Length);
        return audioStreams[t];
    }
    /// <summary>
    /// Gets angle between 2 3d vectors
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="target"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static float GetAngleDegree(Vector3 origin, Vector3 target, float offset = 0)
    {
        var n = (float)(270 - (Mathf.Atan2(origin.Z - target.Z, origin.X - target.X)) * 180 / Mathf.Pi);
        n += offset;
        return n % 360;
    }
    /// <summary>
    /// returns a point in a circle of said radius
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="angleInDegrees"></param>
    /// <param name="origin"></param>
    /// <returns></returns>

    public static Vector2 PointOnCircle(float radius, float angleInDegrees, Vector2 origin)
    {
        // Convert from degrees to radians via multiplication by PI/180        
        float x = (float)(radius * Mathf.Cos(Mathf.DegToRad(angleInDegrees))) + origin.X;
        float y = (float)(radius * Mathf.Sin(Mathf.DegToRad(angleInDegrees))) + origin.Y;

        return new Vector2(x, y);
    }

    /// <summary>
    /// Applies a c# script to a node
    /// </summary>
    /// <param name="holder"></param>
    /// <param name="script"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T AddScriptToNode<T>(ref T holder, string script) where T : Node
    {
        //gets ID. This process is needed due to some issue in godot removing the old object upon replacing script
        ulong objId = holder.GetInstanceId();
        // Replaces old C# instance with a new one. Old C# instance is disposed.
        holder.SetScript(ResourceLoader.Load(script));
        // Get the new C# instance
        // holder = (T)GD.InstanceFromId(objId);
        // holder.Notification(Node.NotificationReady);
        return holder;
    }
    /// <summary>
    /// Adds a space to a sentence for each captial letter
    /// </summary>
    /// <param name="text"></param>
    /// <param name="preserveAcronyms"></param>
    /// <returns></returns>
    public static string AddSpacesToSentence(string text, bool preserveAcronyms = false)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;
        StringBuilder newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            if (char.
            IsUpper(text[i]))
                if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                    (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                     i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                    newText.Append(' ');
            newText.Append(text[i]);
        }
        return newText.ToString();
    }


    /// <summary>
    /// Checks to see if any values in target is null, if so, override by values in source.
    /// This is useful for having a default parameters class, when using editions for an app.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public static void CopyAll<T>(T source, T copy, T target)
    {
        var type = typeof(T);
        foreach (var sourceField in type.GetFields())
        {
            if (sourceField.GetValue(copy) == null)
            {
                var targetField = type.GetField(sourceField.Name);
                targetField.SetValue(target, sourceField.GetValue(source));
            }
            else if (sourceField.GetValue(source).GetType().IsClass && !sourceField.ToString().Contains("System.") && !sourceField.ToString().Contains("Godot."))
            {
                Type _type = sourceField.GetValue(source).GetType();
                FieldInfo[] subFields = _type.GetFields();
                foreach (var _subfield in subFields)
                {
                    if (_subfield.GetValue(sourceField.GetValue(copy)) == null)
                    {
                        var targetField = _type.GetField(_subfield.Name);
                        targetField.SetValue(sourceField.GetValue(target), _subfield.GetValue(sourceField.GetValue(source)));
                    }
                }
            }
        }
    }

    public static Vector3 RandomVector3(Vector3 a, Vector3 b)
    {
        return new Vector3(RandomFloat(a.X, b.X), RandomFloat(a.Y, b.Y), RandomFloat(a.Z, b.Z));
    }

    public static float GetPercentage(ref float deltaValue, float desiredValue, float addedPercentage = 0.1f)
    {
        if (deltaValue == 0)
        {
            return deltaValue = desiredValue;
        }
        else
        {
            return deltaValue = deltaValue * (1 - addedPercentage) + desiredValue * addedPercentage;
        }
    }

    public static Vector2 GetPercentage(ref Vector2 deltaValue, Vector2 desiredValue, float addedPercentage = 0.1f)
    {
        if (deltaValue == Vector2.Zero)
        {
            return deltaValue = desiredValue;
        }
        else
        {
            return deltaValue = deltaValue * (1 - addedPercentage) + desiredValue * addedPercentage;
        }
    }
    public static Vector3 GetPercentage(Vector3 deltaValue, Vector3 desiredValue, float addedPercentage = 0.1f)
    {
        if (deltaValue == Vector3.Zero)
        {
            return desiredValue;
        }
        else
        {
            return deltaValue * (1 - addedPercentage) + desiredValue * addedPercentage;
        }
    }

    /// <summary>
    /// Helpers for specifically Node2Ds
    /// </summary>
    public static class Node2D
    {
        /// <summary>
        /// Applies a shader runtime
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="code"></param>
        public static void ApplyShaderCode(Godot.Node2D obj, string code)
        {
            var mat = new ShaderMaterial();
            mat.Shader = new Shader();
            mat.Shader.Code = code;
            obj.Material = mat;
        }
    }
    /// <summary>
    /// Helpers for specifically Controls
    /// </summary>
    public static class Control
    {

        /// <summary>
        /// Arranges a set of controls into a circle. To use Lerp, it needs to be updated in _Process and given a delta
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="circumfrencePercentage"></param>
        /// <param name="rotation"></param>
        /// <param name="Lerp"></param>
        /// <param name="delta"></param>
        public static void ArrangeControlsInCircle(Godot.Collections.Array<Godot.Node> nodes, Vector2 center, float radius, float circumfrencePercentage = 1, float rotation = 0, bool Lerp = false, float delta = 0)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i] is Godot.Control _control)
                {
                    float angle = 0;
                    if (circumfrencePercentage != 1)
                    {
                        angle = i * Mathf.Pi * circumfrencePercentage * 2f / (nodes.Count - 1) + Mathf.DegToRad(rotation);
                    }
                    else
                    {
                        angle = i * Mathf.Pi * 2f / nodes.Count + Mathf.DegToRad(-90);
                    }
                    Vector2 newPos = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius) + center;
                    Vector2 desiredPos = newPos - _control.Size / 2;
                    _control.Position = Lerp && delta != 0 ? _control.Position.Lerp(desiredPos, delta * 2f) : desiredPos;
                }
            }
        }
        /// <summary>
        /// Applies shader code runtime
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="code"></param>
        public static void ApplyShaderCode(Godot.Control obj, string code)
        {
            var mat = new ShaderMaterial();
            mat.Shader = new Shader();
            mat.Shader.Code = code;
            obj.Material = mat;
        }
        #endregion
    }


}