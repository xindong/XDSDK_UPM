#!/bin/sh
 
echo Unity Version:$2
#UNITY程序的路径#
UNITY_PATH=/Applications/Unity/Hub/Editor/$2/Unity.app/Contents/MacOS/Unity
 
#在Unity中构建apk#
$UNITY_PATH -buildTarget Android -projectPath $1 -executeMethod XDSDK_Editor.ProjectBuild.BuildForAndroid -UNITY_VERSION=$2 -SDK_VERSION=$3 -EXPORT_PATH=$4 -quit

echo "Apk生成完毕" 