#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Activity Definition
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
	/// <see cref="ActivityDefinition" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.ActivityDefinitionData" />.
	/// </summary>
	public abstract class ActivityDefinition : WorkflowObject
	{
		private ActivityDefinitionData mActivityDefinitionData;

		/// <summary>
		/// Initializes a new instance of the <see cref="ActivityDefinition"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="activityDefinitionData"><see cref="T:Tridion.ContentManager.CoreService.Client.ActivityDefinitionData" /></param>
		protected ActivityDefinition(Client client, ActivityDefinitionData activityDefinitionData): base(client, activityDefinitionData)
		{
			if (activityDefinitionData == null)
				throw new ArgumentNullException("activityDefinitionData");

			mActivityDefinitionData = activityDefinitionData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActivityDefinition"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal ActivityDefinition(Client client, TcmUri uri): this(client, client.Read<ActivityDefinitionData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="ActivityDefinition" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.ActivityDefinitionData" />
		/// </summary>
		/// <param name="activityDefinitionData"><see cref="T:Tridion.ContentManager.CoreService.Client.ActivityDefinitionData" /></param>
		protected void Reload(ActivityDefinitionData activityDefinitionData)
		{
			if (activityDefinitionData == null)
				throw new ArgumentNullException("activityDefinitionData");

			mActivityDefinitionData = activityDefinitionData;
			base.Reload(activityDefinitionData);
		}

		/// <summary>
		/// Reload the <see cref="ActivityDefinition" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<ActivityDefinitionData>(this.Id));
		}
	}
}
