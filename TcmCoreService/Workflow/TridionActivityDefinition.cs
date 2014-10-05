#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Tridion Activity Definition
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
	/// <see cref="TridionActivityDefinition" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.TridionActivityDefinitionData" />.
	/// </summary>
	public class TridionActivityDefinition : ActivityDefinition
	{
		private TridionActivityDefinitionData mTridionActivityDefinitionData;

		/// <summary>
		/// Initializes a new instance of the <see cref="TridionActivityDefinition"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="tridionActivityDefinitionData"><see cref="T:Tridion.ContentManager.CoreService.Client.TridionActivityDefinitionData" /></param>
		protected TridionActivityDefinition(Client client, TridionActivityDefinitionData tridionActivityDefinitionData): base(client, tridionActivityDefinitionData)
		{
			if (tridionActivityDefinitionData == null)
				throw new ArgumentNullException("tridionActivityDefinitionData");

			mTridionActivityDefinitionData = tridionActivityDefinitionData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TridionActivityDefinition"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal TridionActivityDefinition(Client client, TcmUri uri): this(client, client.Read<TridionActivityDefinitionData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="TridionActivityDefinition" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.TridionActivityDefinitionData" />
		/// </summary>
		/// <param name="tridionActivityDefinitionData"><see cref="T:Tridion.ContentManager.CoreService.Client.TridionActivityDefinitionData" /></param>
		protected void Reload(TridionActivityDefinitionData tridionActivityDefinitionData)
		{
			if (tridionActivityDefinitionData == null)
				throw new ArgumentNullException("tridionActivityDefinitionData");

			mTridionActivityDefinitionData = tridionActivityDefinitionData;
			base.Reload(tridionActivityDefinitionData);
		}

		/// <summary>
		/// Reload the <see cref="TridionActivityDefinition" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<TridionActivityDefinitionData>(this.Id));
		}
	}
}
