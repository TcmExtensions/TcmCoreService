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
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="tridionProcessDefinitionData"><see cref="T:Tridion.ContentManager.CoreService.Client.TridionProcessDefinitionData" /></param>
		protected TridionProcessDefinition(Client client, TridionProcessDefinitionData tridionProcessDefinitionData): base(client, tridionProcessDefinitionData)
		{
			if (tridionProcessDefinitionData == null)
				throw new ArgumentNullException("tridionProcessDefinitionData");

			mTridionProcessDefinition = tridionProcessDefinitionData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TridionProcessDefinition"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal TridionProcessDefinition(Client client, TcmUri uri): this(client, client.Read<TridionProcessDefinitionData>(uri))
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
			Reload(Client.Read<TridionProcessDefinitionData>(this.Id));
		}


		/// <summary>
		/// Localize this <see cref="TridionProcessDefinition" />
		/// </summary>
		public override void Localize()
		{
			Reload(Client.Localize<TridionProcessDefinitionData>(this.Id));
		}

		/// <summary>
		/// UnLocalize this <see cref="TridionProcessDefinition" />
		/// </summary>
		public override void UnLocalize()
		{
			Reload(Client.UnLocalize<TridionProcessDefinitionData>(this.Id));
		}
	}
}
