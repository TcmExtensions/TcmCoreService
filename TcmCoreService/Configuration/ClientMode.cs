#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Client Mode
// ---------------------------------------------------------------------------------
//	Date Created	: March 19, 2014
//	Author			: Rob van Oostenrijk
// ---------------------------------------------------------------------------------
// 	Change History
//	Date Modified       : 
//	Changed By          : 
//	Change Description  : 
//
////////////////////////////////////////////////////////////////////////////////////
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TcmCoreService.Configuration
{
	/// <summary>
	/// <see cref="ClientMode" /> determines which type of connectivity is used to connect to Tridion
	/// </summary>
	/// <remarks>TcpClient connectivity needs port 2660 to be available on the Content Management server.</remarks>
	public enum ClientMode
	{
		/// <summary>
		/// TCP Connectivity
		/// </summary>
		TcpClient,
		/// <summary>
		/// HTTP Connectivity
		/// </summary>
		HttpClient
	}
}
