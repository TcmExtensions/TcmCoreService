#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Workflow Item
// ---------------------------------------------------------------------------------
//	Date Created	: March 26, 2014
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
using TcmCoreService.Workflow;

namespace TcmCoreService.Interfaces
{
	/// <summary>
	/// <see cref="IWorkflowItem" /> is an interface which defines workflow information for all items capable of participating in a workflow.
	/// </summary>
	public interface IWorkflowItem
	{
		/// <summary>
		/// Retrieves the <see cref="T:TcmCoreService.Workflow.ApprovalStatus" /> of this <see cref="IWorkflowItem" />
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Workflow.ApprovalStatus" /> of this <see cref="IWorkflowItem" />
		/// </value>
		ApprovalStatus ApprovalStatus
		{
			get;
		}

		/// <summary>
		/// Gets <see cref="T:TcmCoreService.Info.Workflow" /> for this <see cref="IWorkflowItem" />.
		/// </summary>
		/// <value>
		/// <see cref="T:TcmCoreService.Info.Workflow" /> for this <see cref="IWorkflowItem" />
		/// </value>
		Info.Workflow Workflow
		{
			get;
		}
	}
}
