[assembly: Parallelizable(ParallelScope.All)]

// It is recommended that LevelOfParallelism number should be the same as number of users in UserManager.cs users pool
// LevelOfParallelism number can be bigger that user pool but need to play with timeouts in .runsettings file
[assembly: LevelOfParallelism(2)]