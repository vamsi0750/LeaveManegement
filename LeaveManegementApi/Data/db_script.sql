-- added users table

 CREATE TABLE [Users] (
          [Id] int NOT NULL IDENTITY,
          [Name] nvarchar(max) NOT NULL,
          [Email] nvarchar(max) NOT NULL,
          [PasswordHash] varbinary(max) NOT NULL,
          [PasswordSalt] varbinary(max) NOT NULL,
          [VerificationToken] nvarchar(max) NULL,
          [VerifiedAt] datetime2 NULL,
          [PasswordResetToken] nvarchar(max) NULL,
          [ResetTokenExpiriesAt] datetime2 NULL,
          CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
      );

--      