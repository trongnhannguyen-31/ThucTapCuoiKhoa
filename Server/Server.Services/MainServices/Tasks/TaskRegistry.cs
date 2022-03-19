using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;

namespace Phoenix.Server.Services.MainServices.Tasks
{
	public class TaskRegistry:Registry
	{
		public TaskRegistry()
		{
            //Schedule<ProductCacheJob>().ToRunNow().AndEvery(24).Hours();
            //Schedule<SendEmailInQueueJob>().ToRunNow();
            //Schedule<AssignOrderToAgencyJob>().ToRunNow().AndEvery(5).Minutes();
            //Schedule<ChangeAgencyJob>().ToRunNow().AndEvery(1).Minutes();
        }
    }
}
