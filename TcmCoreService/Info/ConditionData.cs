#region Header
////////////////////////////////////////////////////////////////////////////////////
//
//	File Description: Condition
// ---------------------------------------------------------------------------------
//	Date Created	: October 9, 2014
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
using Tridion.ContentManager.CoreService.Client;

namespace TcmCoreService.Info
{
    /// <summary>
    /// <see cref="Condition" /> wraps around <see cref="T:Tridion.ContentManager.CoreService.Client.ConditionData" />
    /// </summary>
    public class Condition : InfoBase
    {
		private ConditionData mConditionData;

		/// <summary>
        /// Initializes a new instance of the <see cref="Condition"/> class.
		/// </summary>
		/// <param name="client"><see cref="T:TcmCoreService.Client" /></param>
        /// <param name="targetGroupConditionData"><see cref="T:Tridion.ContentManager.CoreService.Client.ConditionData" /></param>
		internal Condition(Client client, ConditionData conditionData): base(client)
		{
            mConditionData = conditionData;			
		}

		/// <summary>
        /// Gets or sets the negation for this <see cref="Condition" />
		/// </summary>
		/// <value>
        /// Negation for this <see cref="Condition" />
		/// </value>
		public Boolean Negate
		{
			get
			{
                return mConditionData.Negate.GetValueOrDefault(false);
			}
			set
			{
                mConditionData.Negate = value;
            }
		}
    }
}
