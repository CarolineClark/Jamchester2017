public class Constants {
    public enum PlayState {TETRIS, PLATFORMER};

    // events
    public static readonly string tetrisEvent = "tetris-event";
    public static readonly string platformerEvent = "platformer-event";
    public static readonly string cameraFollowingPlayerEvent = "CameraFollowingPlayer";
    public static readonly string cameraWatchingTetrisEvent = "CameraFollowingTetris";
    
    // tags
    public static readonly string playerTag = "Player";
    public static readonly string slowBlockTag = "slow-block";
    public static readonly string invertedBlockTag = "inverted-block";
    public static readonly string teleportBlockTag = "teleport-block";
    public static readonly string iceBlockTag = "ice-block";
    public static readonly string bigJumpBlockTag = "big-jump-block";
}