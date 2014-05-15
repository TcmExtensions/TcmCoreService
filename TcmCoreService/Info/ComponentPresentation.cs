#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Component Presentation
// ---------------------------------------------------------------------------------
//	Date Created	: March 24, 2014
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
using TcmCoreService.CommunicationManagement;
using TcmCoreService.ContentManagement;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Info
{
	/// <summary>
	/// <see cref="ComponentPresentation" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.ComponentPresentationData" />
	/// </summary>
	public class ComponentPresentation : InfoBase
	{
		private ComponentPresentationData mComponentPresentationData;
		private Component mComponent = null;
		private ComponentTemplate mComponentTemplate = null;
		private IEnumerable<TargetGroupCondition> mTargetGroupConditions = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="ComponentPresentation" /> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="componentPresentationData"><see cref="T:Tridion.ContentManager.CoreService.Client.ComponentPresentationData" /></param>
		internal ComponentPresentation(Session session, ComponentPresentationData componentPresentationData): base(session)			
		{
			mComponentPresentationData = componentPresentationData;
		}

		/// <summary>
		/// Gets or sets the <see cref="T:TcmCoreService.ContentManagement.Component" /> for this <see cref="ComponentPresentation" />
		/// </summary>
		/// <value>
		/// The <see cref="T:TcmCoreService.ContentManagement.Component" /> for this <see cref="ComponentPresentation" />
		/// </value>
		public Component Component
		{
			get
			{
				if (mComponent == null)
					mComponent = new Component(Session, mComponentPresentationData.Component.IdRef);

				return mComponent;
			}
			set
			{
				mComponent = value;
				mComponentPresentationData.Component.IdRef = mComponent.Id;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="T:TcmCoreService.ContentManagement.ComponentTemplate" /> for this <see cref="ComponentPresentation" />
		/// </summary>
		/// <value>
		/// The <see cref="T:TcmCoreService.ContentManagement.ComponentTemplate" /> for this <see cref="ComponentPresentation" />
		/// </value>
		public ComponentTemplate ComponentTemplate
		{
			get
			{
				if (mComponentTemplate == null)
					mComponentTemplate = new ComponentTemplate(Session, mComponentPresentationData.ComponentTemplate.IdRef);

				return mComponentTemplate;
			}
			set
			{
				mComponentTemplate = value;
				mComponentPresentationData.ComponentTemplate.IdRef = mComponentTemplate.Id;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="I:System.Collections.Generic.IEnumerable{TcmCoreService.Info.TargetGroupCondition}" /> for this <see cref="ComponentPresentation" />
		/// </summary>
		/// <value>
		/// <see cref="I:System.Collections.Generic.IEnumerable{TcmCoreService.Info.TargetGroupCondition}" /> for this <see cref="ComponentPresentation" />
		/// </value>
		public IEnumerable<TargetGroupCondition> TargetGroupConditions
		{
			get
			{
				if (mTargetGroupConditions == null)
					mTargetGroupConditions = mComponentPresentationData.Conditions.Select(targetGroupConditionData => new TargetGroupCondition(Session, targetGroupConditionData));

				return mTargetGroupConditions;
			}
			set
			{

			}
		}
	}
}
