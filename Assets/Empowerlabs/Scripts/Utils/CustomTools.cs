///Created on 29/8/2016
///B REVANTH PRASAD

using UnityEngine;

namespace Empowerlabs.Utils
{
	public class CustomTools 
	{
		private CustomTools() {}

		public static void LogMessage(object message)
		{
			#if DEBUG
			System.Text.StringBuilder builder = new System.Text.StringBuilder ();
			builder.Append ("[");
			builder.Append (System.DateTime.Now);
			builder.Append ("] : ");
			builder.Append (message);
			Debug.Log (builder);
			#endif
		}
	}
}
