using System;
using System.Runtime.CompilerServices;

namespace MessagingApiTemplate.Utils {

	/// <summary>
	/// ログ出力方法カスタマイズ
	/// </summary>
	public static class Trace {

		private static string Format( string message , string memberName , string sourceFilePath , int sourceLineNumber ) {

			string[] path = sourceFilePath.Split( "\\".ToCharArray() );
			string[] className = path[ path.Length - 1 ].Split( ".cs".ToCharArray() );

			return "[" + className[ 0 ] + "$" + memberName + ":" + sourceLineNumber + "]" + message;

		}

		public static void TraceInformation( 
			string message ,
			[CallerMemberName] string memberName = "" ,
			[CallerFilePath] string sourceFilePath = "" ,
			[CallerLineNumber] int sourceLineNumber = 0 
		)
			=> System.Diagnostics.Trace.TraceInformation( Format( message , memberName , sourceFilePath , sourceLineNumber ) );

		public static void TraceWarning(
			string message ,
			[CallerMemberName] string memberName = "" ,
			[CallerFilePath] string sourceFilePath = "" ,
			[CallerLineNumber] int sourceLineNumber = 0
		)
			=> System.Diagnostics.Trace.TraceWarning( Format( message , memberName , sourceFilePath , sourceLineNumber ) );

		public static void TraceError(
			string message ,
			[CallerMemberName] string memberName = "" ,
			[CallerFilePath] string sourceFilePath = "" ,
			[CallerLineNumber] int sourceLineNumber = 0
		)
			=> System.Diagnostics.Trace.TraceError( Format( message , memberName , sourceFilePath , sourceLineNumber ) );

	}

}