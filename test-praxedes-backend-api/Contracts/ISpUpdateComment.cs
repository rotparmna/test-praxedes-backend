﻿using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Contracts
{
	public interface ISpUpdateComment : IExecuteStoreProcedure<Task, Comment>
    {
	}
}
