#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Publishing Target
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

namespace TcmCoreService.CommunicationManagement
{
	/// <summary>
	/// <see cref="PublishingTarget" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.PublishingTargetData" />.
	/// </summary>
	public abstract class PublishingTarget : SystemWideObject
	{
		private PublishingTargetData mPublishingTargetData;

		/// <summary>
		/// Initializes a new instance of the <see cref="PublishingTarget"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="publishingTargetData"><see cref="T:Tridion.ContentManager.CoreService.Client.PublishingTargetData" /></param>
		protected PublishingTarget(Client client, PublishingTargetData publishingTargetData): base(client, publishingTargetData)
		{
			if (publishingTargetData == null)
				throw new ArgumentNullException("publishingTargetData");

			mPublishingTargetData = publishingTargetData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PublishingTarget"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal PublishingTarget(Client client, TcmUri uri): this(client, client.Read<PublishingTargetData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="PublishingTarget" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.PublishingTargetData" />
		/// </summary>
		/// <param name="publishingTargetData"><see cref="T:Tridion.ContentManager.CoreService.Client.PublishingTargetData" /></param>
		protected void Reload(PublishingTargetData publishingTargetData)
		{
			if (publishingTargetData == null)
				throw new ArgumentNullException("publishingTargetData");

			mPublishingTargetData = publishingTargetData;
			base.Reload(publishingTargetData);
		}

		/// <summary>
		/// Reload the <see cref="PublishingTarget" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<PublishingTargetData>(this.Id));			
		}

		/// <summary>
		/// Gets or sets the <see cref="PublishingTarget" /> description.
		/// </summary>
		/// <value>
		/// The <see cref="PublishingTarget" /> description.
		/// </value>
		public String Description
		{
			get
			{
				return mPublishingTargetData.Description;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mPublishingTargetData.Description = value;
			}
		}
	}
}
