#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Tridion Process Definition
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
	/// <see cref="TridionProcessDefinition" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.TridionProcessDefinitionData" />.
	/// </summary>
	public class TridionProcessDefinition : ProcessDefinition
	{
		private TridionProcessDefinitionData mTridionProcessDefinition;

		/// <summary>
		/// Initializes a new instance of the <see cref="TridionProcessDefinition"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="tridionProcessDefinitionData"><see cref="T:Tridion.ContentManager.CoreService.Client.TridionProcessDefinitionData" /></param>
		protected TridionProcessDefinition(Session session, TridionProcessDefinitionData tridionProcessDefinitionData): base(session, tridionProcessDefinitionData)
		{
			if (tridionProcessDefinitionData == null)
				throw new ArgumentNullException("tridionProcessDefinitionData");

			mTridionProcessDefinition = tridionProcessDefinitionData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TridionProcessDefinition"/> class.
		/// </summary>
		/// <param name="session"><see cref="T:TcmCoreService.Session" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal TridionProcessDefinition(Session session, TcmUri uri): this(session, session.Read<TridionProcessDefinitionData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="TridionProcessDefinition" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.TridionProcessDefinitionData" />
		/// </summary>
		/// <param name="tridionProcessDefinitionData"><see cref="T:Tridion.ContentManager.CoreService.Client.TridionProcessDefinitionData" /></param>
		protected void Reload(TridionProcessDefinitionData tridionProcessDefinitionData)
		{
			if (tridionProcessDefinitionData == null)
				throw new ArgumentNullException("tridionProcessDefinitionData");

			mTridionProcessDefinition = tridionProcessDefinitionData;
			base.Reload(tridionProcessDefinitionData);
		}

		/// <summary>
		/// Reload the <see cref="TridionProcessDefinition" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Session.Read<TridionProcessDefinitionData>(this.Id));
		}


		/// <summary>
		/// Localize this <see cref="TridionProcessDefinition" />
		/// </summary>
		public override void Localize()
		{
			Reload(Session.Localize<TridionProcessDefinitionData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="TridionProcessDefinition" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Session.UnLocalize<TridionProcessDefinitionData>(this.Id));
		}
	}
}
