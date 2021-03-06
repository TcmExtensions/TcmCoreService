﻿#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Activity History
// ---------------------------------------------------------------------------------
//	Date Created	: March 22, 2014
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
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Workflow
{
	/// <summary>
	/// <see cref="ActivityHistory" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.ActivityHistoryData" />.
	/// </summary>
	public class ActivityHistory : Activity
	{
		private ActivityHistoryData mActivityHistoryData;

		/// <summary>
		/// Initializes a new instance of the <see cref="ActivityHistory"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="activityHistoryData"><see cref="T:Tridion.ContentManager.CoreService.Client.ActivityHistoryData" /></param>
		protected ActivityHistory(Client client, ActivityHistoryData activityHistoryData): base(client, activityHistoryData)
		{
			if (activityHistoryData == null)
				throw new ArgumentNullException("activityHistoryData");

			mActivityHistoryData = activityHistoryData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActivityHistory"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal ActivityHistory(Client client, TcmUri uri): this(client, client.Read<ActivityHistoryData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="ActivityHistory" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.ActivityHistoryData" />
		/// </summary>
		/// <param name="activityHistoryData"><see cref="T:Tridion.ContentManager.CoreService.Client.ActivityHistoryData" /></param>
		protected void Reload(ActivityHistoryData activityHistoryData)
		{
			if (activityHistoryData == null)
				throw new ArgumentNullException("activityHistoryData");

			mActivityHistoryData = activityHistoryData;
			base.Reload(activityHistoryData);
		}

		/// <summary>
		/// Reload the <see cref="ActivityHistory" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<ActivityHistoryData>(this.Id));
		}
	}
}
