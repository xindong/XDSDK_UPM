//
//  XDCOMPay.h
//  XDPay
//
//  Created by 杜凯强 on 2017/5/27.
//  Copyright © 2017年 xd. All rights reserved.
//

#import <Foundation/Foundation.h>

#import <UIKit/UIKit.h>

#import "RMStore.h"


#pragma mark - XDComPayProtocol
/**
 支付回调协议，当前版本暂不使用
 */
@protocol XDComPayProtocol <NSObject>

@required


/**
 支付成功
 */
- (void)didGetPaymentSuccessResponse;


/**
 支付取消
 */
- (void)didGetPayCancelResponse;


/**
 支付失败
 */
- (void)didGetPaymentFailResponse:(NSString*)errorInfo;

@end


#pragma mark - XDComPayProduct
/**
 支付道具对象
 */
@interface XDComPayProduct : NSObject

/**
 获取道具实例，请务必填写正确的Key&Value

 @param orderInfo 订单信息
 @param verfiyInfo 订单校验信息
 @return XDComPayProduct
 */
+ (instancetype)getProductInstanceWithDictionary:(NSDictionary*)orderInfo
                                      verfiyInfo:(NSDictionary*)verfiyInfo;

/**
 检测参数是否有效,待完善

 @return BOOL
 */
- (BOOL)parametersValid;

@property (nonatomic,assign) BOOL isGuestPay;

@property (nonatomic,copy) NSDictionary * orderInfo;

@property (nonatomic,copy) NSDictionary * verfiyInfo;

@end


#pragma mark - XDCOMStore
/**
 支付类
 */
@interface XDCOMStore : NSObject

typedef void(^finish)(void);

/**
 支付实例
 
 @return 支付实例
 */
+ (instancetype)getInstance;


/**
 设置唤醒本地订单校验线程，补丁

 @param semaphore dispatch_semaphore_t
 */
- (void)setSemaphore:(dispatch_semaphore_t)semaphore;


/**
 临时补丁

 @param finishhandler 结束支付，移除Loading
 */
- (void)setFinishHandler:(void (^)(void))finishhandler;

/**
 进行支付

 @param product 支付的道具信息
 */
- (void)parepareForProduct:(XDComPayProduct*)product;

/**
 处理支付宝App跳转
 
 @param url URL
 */
- (void)alixPayResultHandler:(NSURL *)url;

@property (nonatomic,assign) id<XDComPayProtocol> delegate;

@property (nonatomic,copy) finish finishHandler;

@end


