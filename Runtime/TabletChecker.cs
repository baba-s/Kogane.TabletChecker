using UnityEngine;

namespace Kogane
{
    /// <summary>
    /// デバイスの種類が電話かタブレットかを判別するクラス
    /// https://forum.unity.com/threads/detecting-between-a-tablet-and-mobile.367274/#post-6719620
    /// </summary>
    public static class TabletChecker
    {
        //==============================================================================
        // プロパティ(static)
        //==============================================================================
        public static TabletCheckerData CurrentData
        {
            get
            {
#if UNITY_IOS
                var generation = UnityEngine.iOS.Device.generation.ToString();
#else
                var generation = string.Empty;
#endif

                var data = new TabletCheckerData
                (
                    isAndroid: Application.platform == RuntimePlatform.Android,
                    generation: generation,
                    width: Screen.width,
                    height: Screen.height,
                    dpi: Screen.dpi
                );

                return data;
            }
        }

        /// <summary>
        /// 現在のデバイスがタブレットの場合 true を返します
        /// </summary>
        public static bool IsTabletCurrent => IsTablet( CurrentData );

        /// <summary>
        /// 現在のデバイスの画面のインチを返します
        /// </summary>
        public static float DeviceDiagonalSizeInInchesCurrent => GetDeviceDiagonalSizeInInches( CurrentData );

        //==============================================================================
        // 関数(static)
        //==============================================================================
        /// <summary>
        /// 現在のデバイスの種類を返します
        /// </summary>
        public static bool IsTablet( in TabletCheckerData data )
        {
            return data.IsAndroid
                    ? IsTabletForAndroid( data )
                    : isTabletForiOS( data )
                ;
        }

        /// <summary>
        /// iOS のデバイスの種類を返します
        /// </summary>
        public static bool isTabletForiOS( in TabletCheckerData data )
        {
            return Application.isEditor
                    ? IsTabletForAndroid( data )
                    : data.Generation.Contains( "iPad" )
                ;
        }

        /// <summary>
        /// Android のデバイスの種類を返します
        /// </summary>
        public static bool IsTabletForAndroid( in TabletCheckerData data )
        {
            var width  = data.Width;
            var height = data.Height;

            // Unity エディタ起動時に以下のエラーが発生しないように
            // いずれかの値が不正な場合は処理に進まないようにしています
            // DivideByZeroException: Attempted to divide by zero.
            if ( width == 0 || height == 0 ) return false;

            // 6.5 インチより大きくてアスペクト比が 2 より小さい場合タブレットと見なします
            // iPhone・iPad のインチやアスペクト比を参考にしています
            // https://uzurea.net/iphone-ipad-resolutions/
            var aspectRatio = width / height;
            var isTablet    = 6.5f < GetDeviceDiagonalSizeInInches( data ) && aspectRatio < 2f;

            return isTablet;
        }

        /// <summary>
        /// デバイスの画面のインチを返します
        /// </summary>
        public static float GetDeviceDiagonalSizeInInches( in TabletCheckerData data )
        {
            // https://answers.unity.com/questions/1231526/getting-device-screen-height-and-width-in-inches.html
            // https://forum.unity.com/threads/finding-physical-screen-size.203017/

            var width          = data.Width;
            var height         = data.Height;
            var dpi            = data.Dpi;
            var screenWidth    = width / dpi;
            var screenHeight   = height / dpi;
            var diagonalInches = Mathf.Sqrt( Mathf.Pow( screenWidth, 2 ) + Mathf.Pow( screenHeight, 2 ) );

            return diagonalInches;
        }
    }
}