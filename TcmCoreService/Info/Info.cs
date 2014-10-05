#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Info
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
using System.Linq;
using System.Text;

namespace TcmCoreService.Info
{
	/// <summary>
	/// <see cref="Info" /> is a base class for all info sub-classes
	/// </summary>
	public abstract class InfoBase
	{
		private Client mClient;

		/// <summary>
		/// Initializes a new instance of the <see cref="InfoBase"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		protected InfoBase(Client client)
		{
			mClient = client;
		}

		/// <summary>
		/// Gets <see cref="T:TcmCoreService.Client" />
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Client" />
		/// </value>
		protected Client Client
		{
			get
			{
				return mClient;
			}
		}
	}
}
