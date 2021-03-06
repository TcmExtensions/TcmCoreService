﻿#region Header
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
using TcmCoreService.Misc;
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
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="componentPresentationData"><see cref="T:Tridion.ContentManager.CoreService.Client.ComponentPresentationData" /></param>
		internal ComponentPresentation(Client client, ComponentPresentationData componentPresentationData): base(client)			
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
					mComponent = new Component(Client, mComponentPresentationData.Component.IdRef);

				return mComponent;
			}
			set
			{
				mComponent = value;
				mComponentPresentationData.Component.IdRef = mComponent.Id;
			}
		}

        /// <summary>
        /// Gets or sets the <see cref="T:TcmCoreService.ContentManagement.Component" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="ComponentPresentation" />
        /// </summary>
        /// <value>
        /// The <see cref="T:TcmCoreService.ContentManagement.Component" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="ComponentPresentation" />
        /// </value>
        public TcmUri ComponentUri
        {
            get
            {
                return mComponentPresentationData.Component.IdRef;
            }
            set
            {
                mComponent = null;

                if (value != null)
                    mComponentPresentationData.Component.IdRef = value;
                else
                    mComponentPresentationData.Component.IdRef = TcmUri.NullUri;
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
					mComponentTemplate = new ComponentTemplate(Client, mComponentPresentationData.ComponentTemplate.IdRef);

				return mComponentTemplate;
			}
			set
			{
				mComponentTemplate = value;
				mComponentPresentationData.ComponentTemplate.IdRef = mComponentTemplate.Id;
			}
		}

        /// <summary>
        /// Gets or sets the <see cref="T:TcmCoreService.ContentManagement.ComponentTemplate" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="ComponentPresentation" />
        /// </summary>
        /// <value>
        /// The <see cref="T:TcmCoreService.ContentManagement.ComponentTemplate" /> <see cref="T:TcmCoreService.Misc.TcmUri" /> for this <see cref="ComponentPresentation" />
        /// </value>
        public TcmUri ComponentTemplateUri
        {
            get
            {
                return mComponentPresentationData.ComponentTemplate.IdRef;
            }
            set
            {
                mComponentTemplate = null;

                if (value != null)
                    mComponentPresentationData.ComponentTemplate.IdRef = value;
                else
                    mComponentPresentationData.ComponentTemplate.IdRef = TcmUri.NullUri;
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
					mTargetGroupConditions = mComponentPresentationData.Conditions.Select(targetGroupConditionData => new TargetGroupCondition(Client, targetGroupConditionData));

				return mTargetGroupConditions;
			}
			set
			{
                mTargetGroupConditions = value;

                if (value != null)
                {
                    mComponentPresentationData.Conditions = value.Select(target => new TargetGroupConditionData()
                    {
                        Negate = target.Negate,
                        TargetGroup = new LinkToTargetGroupData() {  IdRef = target.TargetGroup.Id }
                    }).ToArray();
                }
                else
                    mComponentPresentationData.Conditions = null;
			}
		} 
	}
}
