#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Work Item
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
	/// <see cref="WorkItem" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.WorkItemData" />.
	/// </summary>
	public class WorkItem : WorkflowObject
	{
		private WorkItemData mWorkItemData;



		/// <summary>
		/// Initializes a new instance of the <see cref="WorkItem"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="workItemData"><see cref="T:Tridion.ContentManager.CoreService.Client.WorkItemData" /></param>
		protected WorkItem(Client client, WorkItemData workItemData): base(client, workItemData)
		{
			if (workItemData == null)
				throw new ArgumentNullException("workItemData");

			mWorkItemData = workItemData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkItem"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal WorkItem(Client client, TcmUri uri): this(client, client.Read<WorkItemData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="WorkItem" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.WorkItemData" />
		/// </summary>
		/// <param name="workItemData"><see cref="T:Tridion.ContentManager.CoreService.Client.WorkItemData" /></param>
		protected void Reload(WorkItemData workItemData)
		{
			if (workItemData == null)
				throw new ArgumentNullException("workItemData");

			mWorkItemData = workItemData;
			base.Reload(workItemData);
		}

		/// <summary>
		/// Reload the <see cref="WorkItem" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<WorkItemData>(this.Id));
		}
	}
}
