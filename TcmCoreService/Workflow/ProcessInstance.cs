using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TcmCoreService.Misc;
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Workflow
{
	/// <summary>
	/// <see cref="ProcessInstance" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.ProcessInstanceData" />.
	/// </summary>
	public class ProcessInstance : Process
	{
		private ProcessInstanceData mProcessInstanceData;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProcessInstance"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="processInstanceData"><see cref="T:Tridion.ContentManager.CoreService.Client.ProcessInstanceData" /></param>
		protected ProcessInstance(Client client, ProcessInstanceData processInstanceData): base(client, processInstanceData)
		{
			if (processInstanceData == null)
				throw new ArgumentNullException("processInstanceData");

			mProcessInstanceData = processInstanceData;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ProcessInstance"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
		/// <param name="uri"><see cref="T:TcmCoreService.Misc.TcmUri" /></param>
		internal ProcessInstance(Client client, TcmUri uri): this(client, client.Read<ProcessInstanceData>(uri))
		{
		}

		/// <summary>
		/// Reload the <see cref="ProcessInstance" /> with the specified <see cref="T:Tridion.ContentManager.CoreService.Client.ProcessInstanceData" />
		/// </summary>
		/// <param name="processInstanceData"><see cref="T:Tridion.ContentManager.CoreService.Client.ProcessInstanceData" /></param>
		protected void Reload(ProcessInstanceData processInstanceData)
		{
			if (processInstanceData == null)
				throw new ArgumentNullException("processInstanceData");

			mProcessInstanceData = processInstanceData;
			base.Reload(processInstanceData);
		}

		/// <summary>
		/// Reload the <see cref="ProcessInstance" /> data from the Content Manager
		/// </summary>
		public override void Reload()
		{
			Reload(Client.Read<ProcessInstanceData>(this.Id));
		}
	}
}
