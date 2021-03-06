﻿#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Publish Transaction
// ---------------------------------------------------------------------------------
//	Date Created	: March 21, 2014
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
using TcmCoreService.ContentManagement;
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Publishing
{
	/// <summary>
	/// <see cref="PublishTransaction" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.PublishTransactionData" />.
	/// </summary>
	public sealed class PublishTransaction : SystemWideObject
	{
		private PublishTransactionData mPublishTransactionData;

		/// <summary>
		/// Initializes a new instance of the <see cref="PublishTransaction"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="publishTransactionData"><see cref="T:Tridion.ContentManager.CoreService.Client.PublishTransactionData" /></param>
		private PublishTransaction(Client client, PublishTransactionData publishTransactionData): base(client, publishTransactionData)
		{
			if (publishTransactionData == null)
				throw new ArgumentNullException("publishTransactionData");

			mPublishTransactionData = publishTransactionData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PublishTransaction"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal PublishTransaction(Client client, TcmUri uri): this(client, client.Read<PublishTransactionData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="PublishTransaction" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.PublishTransactionData" />
		/// </summary>
		/// <param name="publishTransactionData"><see cref="T:Tridion.ContentManager.CoreService.Client.PublishTransactionData" /></param>
		private void Reload(PublishTransactionData publishTransactionData)
		{
			if (publishTransactionData == null)
				throw new ArgumentNullException("publishTransactionData");

			mPublishTransactionData = publishTransactionData;
			base.Reload(publishTransactionData);
		}

		/// <summary>
		/// Reload the <see cref="PublishTransaction" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<PublishTransactionData>(this.Id));
		}
	}
}
