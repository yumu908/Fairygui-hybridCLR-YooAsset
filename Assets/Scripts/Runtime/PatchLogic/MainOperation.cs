﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniFramework.Machine;
using UniFramework.Event;
using YooAsset;

public class MainOperation : GameAsyncOperation
{

    private enum ESteps
    {
        None,
        Update,
        Done,
    }

    private readonly EventGroup _eventGroup = new EventGroup();
    protected readonly StateMachine _machine;
    private ESteps _steps = ESteps.None;

    public MainOperation(string packageName, string buildPipeline, EPlayMode playMode)
    {
        // 注册监听事件
        _eventGroup.AddListener<UserEventDefine.UserTryInitialize>(OnHandleEventMessage);
        _eventGroup.AddListener<UserEventDefine.UserBeginDownloadWebFiles>(OnHandleEventMessage);
        _eventGroup.AddListener<UserEventDefine.UserTryUpdatePackageVersion>(OnHandleEventMessage);
        _eventGroup.AddListener<UserEventDefine.UserTryUpdatePatchManifest>(OnHandleEventMessage);
        _eventGroup.AddListener<UserEventDefine.UserTryDownloadWebFiles>(OnHandleEventMessage);


        _eventGroup.AddListener<PatchEventDefine.InitializeFailed>(OnHandleEventMessage);
        _eventGroup.AddListener<PatchEventDefine.PatchStatesChange>(OnHandleEventMessage);
        _eventGroup.AddListener<PatchEventDefine.FoundUpdateFiles>(OnHandleEventMessage);
        _eventGroup.AddListener<PatchEventDefine.DownloadProgressUpdate>(OnHandleEventMessage);
        _eventGroup.AddListener<PatchEventDefine.PackageVersionUpdateFailed>(OnHandleEventMessage);
        _eventGroup.AddListener<PatchEventDefine.PatchManifestUpdateFailed>(OnHandleEventMessage);
        _eventGroup.AddListener<PatchEventDefine.WebFileDownloadFailed>(OnHandleEventMessage);

        // 创建状态机
        _machine = new StateMachine(this);
        _machine.AddNode<FsmInitializePackage>();
        _machine.AddNode<FsmUpdatePackageVersion>();
        _machine.AddNode<FsmUpdatePackageManifest>();
        _machine.AddNode<FsmCreatePackageDownloader>();
        _machine.AddNode<FsmDownloadPackageFiles>();
        _machine.AddNode<FsmDownloadPackageOver>();
        _machine.AddNode<FsmClearPackageCache>();
        _machine.AddNode<FsmUpdaterDone>();

        _machine.SetBlackboardValue("PackageName", packageName);
        _machine.SetBlackboardValue("PlayMode", playMode);
        _machine.SetBlackboardValue("BuildPipeline", buildPipeline);
    }
    protected override void OnStart()
    {
        _steps = ESteps.Update;
        _machine.Run<FsmInitializePackage>();
    }
    protected override void OnUpdate()
    {
        if (_steps == ESteps.None || _steps == ESteps.Done)
            return;

        if (_steps == ESteps.Update)
        {
            _machine.Update();
            if (_machine.CurrentNode == typeof(FsmUpdaterDone).FullName)
            {
                _eventGroup.RemoveAllListener();
                Status = EOperationStatus.Succeed;
                _steps = ESteps.Done;
            }
        }
    }
    protected override void OnAbort()
    {
    }

    /// <summary>
    /// 接收事件
    /// </summary>
    protected virtual void OnHandleEventMessage(IEventMessage message)
    {
        if (message is UserEventDefine.UserTryInitialize)
        {
            _machine.ChangeState<FsmInitializePackage>();
        }
        else if (message is UserEventDefine.UserBeginDownloadWebFiles)
        {
            _machine.ChangeState<FsmDownloadPackageFiles>();
        }
        else if (message is UserEventDefine.UserTryUpdatePackageVersion)
        {
            _machine.ChangeState<FsmUpdatePackageVersion>();
        }
        else if (message is UserEventDefine.UserTryUpdatePatchManifest)
        {
            _machine.ChangeState<FsmUpdatePackageManifest>();
        }
        else if (message is UserEventDefine.UserTryDownloadWebFiles)
        {
            _machine.ChangeState<FsmCreatePackageDownloader>();
        }
        else if (message is PatchEventDefine.InitializeFailed)
        {
            Debug.Log("Failed to initialize package !");
        }
        else if (message is PatchEventDefine.PatchStatesChange)
        {

        }
        else if (message is PatchEventDefine.FoundUpdateFiles)
        {
            UserEventDefine.UserBeginDownloadWebFiles.SendEventMessage();
        }
        else if (message is PatchEventDefine.DownloadProgressUpdate)
        {

        }
        else if (message is PatchEventDefine.PackageVersionUpdateFailed)
        {
            UserEventDefine.UserTryUpdatePackageVersion.SendEventMessage();
        }
        else if (message is PatchEventDefine.PatchManifestUpdateFailed)
        {
            UserEventDefine.UserTryUpdatePatchManifest.SendEventMessage();
        }
        else if (message is PatchEventDefine.WebFileDownloadFailed)
        {
            UserEventDefine.UserTryDownloadWebFiles.SendEventMessage();
        }
        else
        {
            throw new System.NotImplementedException($"{message.GetType()}");
        }
    }
}