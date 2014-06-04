﻿#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Target Destinaton
// ---------------------------------------------------------------------------------
//	Date Created	: March 26, 2014
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
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Info
{
	/// <summary>
	/// <see cref="TargetDestination" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.TargetDestinationData" />
	/// </summary>
	public class TargetDestination : InfoBase
	{
		private TargetDestinationData mTargetDestinationData;

		private Schema mProtocolSchema = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="TargetDestination"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="binaryContentData"><see cref="T:Tridion.ContentManager.CoreService.Client.TargetDestinationData" /></param>
		public TargetDestination(Session session, TargetDestinationData targetDestinationData): base(session)
		{
			mTargetDestinationData = targetDestinationData;			
		}

		/// <summary>
		/// Gets or sets <see cref="TargetDestination" /> protocol data.
		/// </summary>
		/// <value>
		/// <see cref="TargetDestination" /> protocol data.
		/// </value>
		public String ProtocolData
		{
			get
			{
				return mTargetDestinationData.ProtocolData;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mTargetDestinationData.ProtocolData = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="TargetDestination" /> protocol <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </summary>
		/// <value>
		/// <see cref="TargetDestination" /> protocol <see cref="T:TcmCoreService.ContentManagement.Schema" />
		/// </value>
		public Schema ProtocolSchema
		{
			get
			{
				if (mProtocolSchema == null)
					mProtocolSchema = new Schema(Session, mTargetDestinationData.ProtocolSchema.IdRef);

				return mProtocolSchema;
			}
			set
			{
				mProtocolSchema = value;
				mTargetDestinationData.ProtocolSchema.IdRef = mProtocolSchema.Id;
			}
		}

		/// <summary>
		/// Gets or sets <see cref="TargetDestination" /> title.
		/// </summary>
		/// <value>
		/// <see cref="TargetDestination" /> title.
		/// </value>
		public String Title
		{
			get
			{
				return mTargetDestinationData.Title;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mTargetDestinationData.Title = value;
			}
		}
	}
}