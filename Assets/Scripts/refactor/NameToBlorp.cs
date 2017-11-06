using UnityEngine;
using System.Collections;
 
// example class of how to use reflections
// source https://answers.unity.com/questions/754412/getting-variables-by-their-names.html
public class NameToBlorp : MonoBehaviour {
 
	// Find the closest point on AB to a point
	void Awake()
	{
		// Examining the name of all variables in a C# object
		// In this case, we'll list the variable in this NameToBlorp
		// class
		System.Reflection.PropertyInfo [] rProps = this.GetType().GetProperties();
		foreach(System.Reflection.PropertyInfo rp in rProps )
			Debug.Log( rp.Name );
 
		// Getting the info of a specific variable name.
		// This gives us the ability to read/write it
		System.Reflection.PropertyInfo propName = this.GetType().GetProperty( "name" );
		if( propName != null )
		{
			// The PropertyInfo isn't the actual variable, just the "idea" of
			// the variable existing in an object.
			// 
			// It needs to be used in conjunction with the object...
			// Equivalent of this.name = "blorp"
			propName.SetValue(     
				this, // So we specify who owns the object
				"blorp", // A C# object as the value, will be casted (if possible)
				null
			);
 
			// And GetValue can be used in a similar fassion.
			// Equivalent of Debug.log( "..." + this.name )
			Debug.Log( "The name is " + propName.GetValue( this, null ) );
		}
 
	}
}