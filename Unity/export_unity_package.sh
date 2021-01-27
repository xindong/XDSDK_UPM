#!/bin/sh

echo Unity Version:$2
#UNITY程序的路径#
UNITY_PATH=/Applications/Unity/Hub/Editor/$2/Unity.app/Contents/MacOS/Unity

echo SDK_VERSION:$3

echo EXPORT_PATH:$4

$UNITY_PATH -projectPath $1 -batchmode -executeMethod XDSDK_Editor.ProjectBuild.ExportUnityPackage -UNITY_VERSION=$2 -SDK_VERSION=$3 -EXPORT_PATH=$4 -quit