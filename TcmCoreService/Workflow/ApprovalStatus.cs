﻿#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Approval Status
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
using TcmCoreService.ContentManagement;
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Workflow
{
	/// <summary>
	/// <see cref="ApprovalStatus" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.ApprovalStatusData" />.
	/// </summary>
	public class ApprovalStatus : SystemWideObject
	{
		private ApprovalStatusData mApprovalStatusData;

		/// <summary>
		/// Initializes a new instance of the <see cref="ApprovalStatus"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="activityData"><see cref="T:Tridion.ContentManager.CoreService.Client.ApprovalStatusData" /></param>
		protected ApprovalStatus(Client client, ApprovalStatusData approvalStatusData): base(client, approvalStatusData)
		{
			if (approvalStatusData == null)
				throw new ArgumentNullException("approvalStatusData");

			mApprovalStatusData = approvalStatusData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ApprovalStatus"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal ApprovalStatus(Client client, TcmUri uri): this(client, client.Read<ApprovalStatusData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="ApprovalStatus" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.ApprovalStatusData" />
		/// </summary>
		/// <param name="approvalStatusData"><see cref="T:Tridion.ContentManager.CoreService.Client.ApprovalStatusData" /></param>
		protected void Reload(ApprovalStatusData approvalStatusData)
		{
			if (approvalStatusData == null)
				throw new ArgumentNullException("approvalStatusData");

			mApprovalStatusData = approvalStatusData;
			base.Reload(approvalStatusData);
		}

		/// <summary>
		/// Reload the <see cref="ApprovalStatus" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<ApprovalStatusData>(this.Id));
		}
	}
}
