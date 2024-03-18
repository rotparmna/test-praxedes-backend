﻿using test_praxedes_backend_api.Models;

namespace test_praxedes_backend_api.Contracts
{
    public interface IInsertBulkPostComment
    {
        Task InsertBulk(List<Post> posts, List<Comment> comments);
    }
}