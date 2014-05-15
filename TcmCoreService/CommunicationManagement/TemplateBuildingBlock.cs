﻿#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Template Building Block
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
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.CommunicationManagement
{
	/// <summary>
	/// <see cref="TemplateBuildingBlock" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.TemplateBuildingBlockData" />.
	/// </summary>
	public class TemplateBuildingBlock : Template
	{
		private TemplateBuildingBlockData mTemplateBuildingBlockData;

		/// <summary>
		/// Initializes a new instance of the <see cref="TemplateBuildingBlock"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="templateData"><see cref="T:Tridion.ContentManager.CoreService.Client.TemplateBuildingBlockData" /></param>
		protected TemplateBuildingBlock(Session session, TemplateBuildingBlockData templateBuildingBlockData): base(session, templateBuildingBlockData)
		{
			if (templateBuildingBlockData == null)
				throw new ArgumentNullException("templateBuildingBlockData");

			mTemplateBuildingBlockData = templateBuildingBlockData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TemplateBuildingBlock"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal TemplateBuildingBlock(Session session, TcmUri uri): this(session, session.Read<TemplateBuildingBlockData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="TemplateBuildingBlock" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.TemplateBuildingBlockData" />
		/// </summary>
		/// <param name="templateBuildingBlockData"><see cref="T:Tridion.ContentManager.CoreService.Client.TemplateBuildingBlockData" /></param>
		protected void Reload(TemplateBuildingBlockData templateBuildingBlockData)
		{
			if (templateBuildingBlockData == null)
				throw new ArgumentNullException("templateBuildingBlockData");

			mTemplateBuildingBlockData = templateBuildingBlockData;
			base.Reload(templateBuildingBlockData);
		}

		/// <summary>
		/// Reload the <see cref="TemplateBuildingBlock" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Session.Read<TemplateBuildingBlockData>(this.Id));
		}

		/// <summary>
		/// Localize this <see cref="TemplateBuildingBlock" />
		/// </summary>
		public override void Localize()
		{
			Reload(Session.Localize<TemplateBuildingBlockData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="TemplateBuildingBlock" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Session.UnLocalize<TemplateBuildingBlockData>(this.Id));
		}
	}
}
