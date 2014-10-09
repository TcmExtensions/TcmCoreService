#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Target Group Condition
// ---------------------------------------------------------------------------------
//	Date Created	: October 9, 2014
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
using TcmCoreService.AudienceManagement;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Info
{
	/// <summary>
	/// <see cref="TargetGroupCondition" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.TargetGroupConditionData" />
	/// </summary>
	public class TargetGroupCondition : Condition
	{
		private TargetGroupConditionData mTargetGroupConditionData;
		private TargetGroup mTargetGroup = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="TargetGroupCondition"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="targetGroupConditionData"><see cref="T:Tridion.ContentManager.CoreService.Client.TargetGroupConditionData" /></param>
		internal TargetGroupCondition(Client client, TargetGroupConditionData targetGroupConditionData): base(client, targetGroupConditionData)
		{
			mTargetGroupConditionData = targetGroupConditionData;			
		}

		/// <summary>
		/// Gets or sets the <see cref="T:TcmCoreService.AudienceManagement.TargetGroup" /> for this <see cref="TargetGroupCondition" />
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.AudienceManagement.TargetGroup" /> for this <see cref="TargetGroupCondition" />
		/// </value>
		public TargetGroup TargetGroup
		{
			get
			{
				if (mTargetGroup == null)
					mTargetGroup = new TargetGroup(Client, mTargetGroupConditionData.TargetGroup.IdRef);

				return mTargetGroup;
			}
			set
			{
				mTargetGroup = null;
				mTargetGroupConditionData.TargetGroup.IdRef = mTargetGroup.Id;
			}
		}
	}
}
