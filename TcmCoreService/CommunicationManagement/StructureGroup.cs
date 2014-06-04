﻿#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Structure Group
// ---------------------------------------------------------------------------------
//	Date Created	: March 21, 2014
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
using TcmCoreService.Workflow;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.CommunicationManagement
{
	/// <summary>
	/// <see cref="StructureGroup" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.StructureGroupData" />.
	/// </summary>
	public class StructureGroup : OrganizationalItem
	{
		private StructureGroupData mStructureGroupData;

		private PageTemplate mDefaultPageTemplate = null;
		private ProcessDefinition mPageProcess = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="StructureGroup"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="structureGroupData"><see cref="T:Tridion.ContentManager.CoreService.Client.StructureGroupData" /></param>
		protected StructureGroup(Session session, StructureGroupData structureGroupData): base(session, structureGroupData)
		{
			if (structureGroupData == null)
				throw new ArgumentNullException("structureGroupData");

			mStructureGroupData = structureGroupData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StructureGroup"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal StructureGroup(Session session, TcmUri uri): this(session, session.Read<StructureGroupData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="StructureGroup" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.StructureGroupData" />
		/// </summary>
		/// <param name="structureGroupData"><see cref="T:Tridion.ContentManager.CoreService.Client.StructureGroupData" /></param>
		protected void Reload(StructureGroupData structureGroupData)
		{
			if (structureGroupData == null)
				throw new ArgumentNullException("structureGroupData");

			mStructureGroupData = structureGroupData;
			base.Reload(structureGroupData);

			mDefaultPageTemplate = null;
			mPageProcess = null;
		}

		/// <summary>
		/// Reload the <see cref="StructureGroup" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Session.Read<StructureGroupData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="StructureGroup" />
		/// </summary>
		public override void Localize()
		{
			Reload(Session.Localize<StructureGroupData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="StructureGroup" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Session.UnLocalize<StructureGroupData>(this.Id));			
		}

		/// <summary>
		/// Gets or sets the default <see cref="T:TcmCoreService.CommunicationManagement.PageTemplate" /> for this <see cref="StructureGroup" />
		/// </summary>
		/// <value>
		/// Default <see cref="T:TcmCoreService.CommunicationManagement.PageTemplate" /> for this <see cref="StructureGroup" />
		/// </value>
		public PageTemplate DefaultPageTemplate
		{
			get
			{
				if (mDefaultPageTemplate == null)
					mDefaultPageTemplate = new PageTemplate(Session, mStructureGroupData.DefaultPageTemplate.IdRef);

				return mDefaultPageTemplate;
			}
			set
			{
				mDefaultPageTemplate = value;

				if (value != null)
					mStructureGroupData.DefaultPageTemplate.IdRef = value.Id;
				else
					mStructureGroupData.DefaultPageTemplate.IdRef = TcmUri.NullUri;				
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="StructureGroup" /> directory.
		/// </summary>
		/// <value>
		/// The <see cref="StructureGroup" /> directory.
		/// </value>
		public String Directory
		{
			get
			{
				return mStructureGroupData.Directory;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
					mStructureGroupData.Directory = value;				
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="StructureGroup" /> is active.
		/// </summary>
		/// <value>
		///   <c>true</c> if this <see cref="StructureGroup" /> is active; otherwise, <c>false</c>.
		/// </value>
		public Boolean IsActive
		{
			get
			{
				return mStructureGroupData.IsActive.GetValueOrDefault(false);
			}
			set
			{
				mStructureGroupData.IsActive = value;				
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="StructureGroup" /> is active resolved.
		/// </summary>
		/// <value>
		/// <c>true</c> if this <see cref="StructureGroup" /> is active resolved; otherwise, <c>false</c>.
		/// </value>
		public Boolean IsActiveResolvedValue
		{
			get
			{
				return mStructureGroupData.IsActiveResolvedValue.GetValueOrDefault(false);
			}
			set
			{
				mStructureGroupData.IsActiveResolvedValue = value;				
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="StructureGroup" /> has a default inherited <see cref="T:TcmCoreService.CommunicationManagement.PageTemplate" />
		/// </summary>
		/// <value>
		/// <c>true</c> if this <see cref="StructureGroup" /> has a default inherited <see cref="T:TcmCoreService.CommunicationManagement.PageTemplate" />; otherwise, <c>false</c>.
		/// </value>
		public Boolean IsDefaultPageTemplateInherited
		{
			get
			{
				return mStructureGroupData.IsDefaultPageTemplateInherited.GetValueOrDefault(false);
			}
			set
			{
				mStructureGroupData.IsDefaultPageTemplateInherited = value;				
			}
		}

		public ProcessDefinition PageProcess
		{
			get
			{
				if (mPageProcess == null)
					mPageProcess = new TridionProcessDefinition(Session, mStructureGroupData.PageProcess.IdRef);

				return mPageProcess;
			}
			set
			{
				mPageProcess = value;

				if (value != null)
					mStructureGroupData.PageProcess.IdRef = value.Id;
				else
					mStructureGroupData.PageProcess.IdRef = TcmUri.NullUri;
			}
		}
	}
}