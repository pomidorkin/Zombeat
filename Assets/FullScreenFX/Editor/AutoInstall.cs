namespace FullScreenFX.AutoInstall 
{
    using UnityEngine;
    using UnityEditor; 
    public static class AutoInstall
    {
        const string INSTALL_MARK = "FullScreenFX/Editor/Speed_v2.0.0_installed";
        const string URP = "Assets/FullScreenFX/Packages/Speed_URP.unitypackage";
        const string HDRP = "Assets/FullScreenFX/Packages/Speed_HDRP.unitypackage";
        const string BUILTIN = null;
        
        [UnityEditor.Callbacks.DidReloadScripts]
        static void RunAutoInstall()
        {
            string pathToInstalledFile = Application.dataPath + "/" + INSTALL_MARK;
            if (System.IO.File.Exists(pathToInstalledFile))
            {
                return;
            }
            var package = DetectPackage();
            if (!string.IsNullOrEmpty(package))
            {
                AssetDatabase.ImportPackage(package, false);
                System.IO.File.WriteAllText(pathToInstalledFile, string.Empty);
            }
        }

        static string DetectPackage()
        {
            #if USING_URP 
            return URP;
            #elif USING_HDRP
            return HDRP;
            #else
            return BUILTIN;
            #endif
        }
    }
}