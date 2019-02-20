using System;

namespace ummisco.gama.unity.notification
{
    public class NotificationEntry
	{
		
		public string notificationId { get; set; }

		public string objectName { get; set; }

		public string fieldType { get; set; }

		public string fieldName { get; set; }

		public object fieldValue { get; set; }

		public string fieldOperator { get; set; }

		public string agentId { get; set; }

		public Boolean notify  { get; set; }

		public Boolean isSent  { get; set; }

		public NotificationEntry (string notificationId, string objectName, string fieldType, string fieldName, object fieldValue, string fieldOperator, string agentId)
		{
			this.notificationId = notificationId;
			this.objectName = objectName;
			this.fieldType = fieldType;
			this.fieldName = fieldName;
			this.fieldValue = fieldValue;
			this.fieldOperator = fieldOperator;
			this.agentId = agentId;
			this.notify = false;
			this.isSent = false;
		}

		public NotificationEntry ()
		{
		
		}




	}
}

