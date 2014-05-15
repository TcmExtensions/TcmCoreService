#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Activity Instance
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
	/// <see cref="ActivityInstance" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.ActivityInstanceData" />.
	/// </summary>
	public class ActivityInstance : Activity
	{
		private ActivityInstanceData mActivityInstanceData;

		/// <summary>
		/// Initializes a new instance of the <see cref="ActivityInstance"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="activityInstanceData"><see cref="T:Tridion.ContentManager.CoreService.Client.ActivityInstanceData" /></param>
		protected ActivityInstance(Session session, ActivityInstanceData activityInstanceData): base(session, activityInstanceData)
		{
			if (activityInstanceData == null)
				throw new ArgumentNullException("activityInstanceData");

			mActivityInstanceData = activityInstanceData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActivityInstance"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal ActivityInstance(Session session, TcmUri uri): this(session, session.Read<ActivityInstanceData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="ActivityInstance" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.ActivityInstanceData" />
		/// </summary>
		/// <param name="activityInstanceData"><see cref="T:Tridion.ContentManager.CoreService.Client.ActivityInstanceData" /></param>
		protected void Reload(ActivityInstanceData activityInstanceData)
		{
			if (activityInstanceData == null)
				throw new ArgumentNullException("activityInstanceData");

			mActivityInstanceData = activityInstanceData;
			base.Reload(activityInstanceData);
		}

		/// <summary>
		/// Reload the <see cref="ActivityInstance" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Session.Read<ActivityInstanceData>(this.Id));
		}
	}
}
