#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Target Group
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

namespace TcmCoreService.AudienceManagement
{
	/// <summary>
	/// <see cref="TargetGroup" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.TargetGroupData" />.
	/// </summary>
	public class TargetGroup : RepositoryLocalObject
	{
		private TargetGroupData mTargetGroupData;

		/// <summary>
		/// Initializes a new instance of the <see cref="TargetGroup"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="targetGroupData"><see cref="T:Tridion.ContentManager.CoreService.Client.TargetGroupData" /></param>
		protected TargetGroup(Client client, TargetGroupData targetGroupData): base(client, targetGroupData)
		{
			if (targetGroupData == null)
				throw new ArgumentNullException("targetGroupData");

			mTargetGroupData = targetGroupData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TargetGroup"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal TargetGroup(Client client, TcmUri uri): this(client, client.Read<TargetGroupData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="TargetGroup" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.TargetGroupData" />
		/// </summary>
		/// <param name="targetGroupData"><see cref="T:Tridion.ContentManager.CoreService.Client.TargetGroupData" /></param>
		protected void Reload(TargetGroupData targetGroupData)
		{
			if (targetGroupData == null)
				throw new ArgumentNullException("targetGroupData");

			mTargetGroupData = targetGroupData;
			base.Reload(targetGroupData);
		}

		/// <summary>
		/// Reload the <see cref="TargetGroup" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<TargetGroupData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="TargetGroup" />
		/// </summary>
		public override void Localize()
		{
			Reload(Client.Localize<TargetGroupData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="TargetGroup" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Client.UnLocalize<TargetGroupData>(this.Id));			
		}

		/// <summary>
		/// Gets the <see cref="I:System.Collections.Generic{ConditionData}" /> for this <see cref="TargetGroup" />
		/// </summary>
		/// <value>
		/// <see cref="I:System.Collections.Generic{ConditionData}" /> for this <see cref="TargetGroup" />
		/// </value>
		public IEnumerable<ConditionData> Conditions
		{
			get
			{
				return mTargetGroupData.Conditions;
			}
		}

		/// <summary>
		/// Retrieve the <see cref="TargetGroup" /> description.
		/// </summary>
		/// <value>
		/// <see cref="TargetGroup" /> description.
		/// </value>
		public String Description
		{
			get
			{
				return mTargetGroupData.Description;
			}
		}
	}
}
