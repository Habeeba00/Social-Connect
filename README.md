Project Name: SocialConnect API
Description:
Backend API for a social media platform enabling user interactions (posts, comments, likes) with secure, scalable, and user-friendly features.

Key Features:
User Management:
Register, login, update profiles, follow/unfollow users.

Post Management:
Create, view, update, delete posts; fetch user feeds.

Comment Management:
Add, view, delete comments (owner/admin only).

Like Management:
Like/unlike posts; view users who liked a post.


Technical Details:
Data Format: JSON.
Database: SQL Server (users, posts, comments, likes).
Authentication: JWT-based.
Authorization: Role-based (users edit own content; admins manage all).
Error Handling: Standard HTTP codes with descriptive messages.
Documentation: Swagger/OpenAPI.

Example Endpoints:
Users:
POST /users/register, POST /users/login, GET /users/{id}.

Posts:
GET /posts, POST /posts, DELETE /posts/{id}.

Comments:
POST /posts/{post_id}/comments, GET /posts/{post_id}/comments.

Likes:
POST /posts/{id}/like, GET /posts/{id}/likes.


Tools & Technologies:
Backend: ASP.NET Core (or preferred framework).
Database: SQL Server.
Authentication: JWT.
Docs: Swagger.
Version Control: Git/GitHub.

Purpose:
Provide a secure, scalable backend for social media platforms, enabling core functionalities for developers to build engaging user experiences.
