﻿using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Contracts
{
	public interface ISpGetCommentById : IExecuteStoreProcedure<Task<Comment>, int>
    {
	}
}

