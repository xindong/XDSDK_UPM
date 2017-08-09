

#import "XDSDK.h"

#import <XdComPlatform/XDCore.h>

@interface XDSDK ()<XDCallback>

@property (nonatomic,copy,nonnull)NSString* gameObjectName;

@end

@implementation XDSDK

static XDSDK * instance;

# pragma mark - XDSDKWrapperSettings

/**
 Instacne

 @return XDSDK
 */
+ (instancetype)defaultInstance{
    
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        
        instance = [[XDSDK alloc] init];
    });
    
    return instance;
}


/**
 设置GameObject

 @param gameObject GameObjectName
 */
+ (void)setGameObject:(nonnull NSString*)gameObject{
    
    if (!gameObject ||![gameObject isKindOfClass:[NSString class]]) {
        
        return;
    }
    
    [[XDSDK defaultInstance] setGameObjectName:gameObject];
}

# pragma mark - XDSDKWrapper

#if __cplusplus
    extern "C" {
#endif
        
        void initSDK(const char* appid,int orientation){
            
            XDSDK * sdk = [XDSDK defaultInstance];
            
            [XDSDK setGameObject:@"XDSDK"];
            
            [XDCore setCallBack:sdk];
            
            [XDCore init:[NSString stringWithUTF8String:appid] orientation:orientation];
        }
        
        void login(){
            
            [XDCore login];
        }
        
        void logout(){
            
            [XDCore logout];
        }
        
        bool openUserCenter(){
            
            return [XDCore openUserCenter];
        }
        
        bool isLoggedIn(){
            
            return [XDCore isLoggedIn];
        }
        
        void pay(const char* product_name, const char* product_id, const char* product_price,const char* sid,const char* role_id,const char* orderid,const char* ext){
            
            [XDCore pay:@{
                          @"Product_Name":[NSString stringWithUTF8String:product_name?product_name:"Product_Name"],
                          @"Product_Id":[NSString stringWithUTF8String:product_id?product_id:"Product_Id"],
                          @"Product_Price":[NSString stringWithUTF8String:product_price?product_price:"Product_Price"],
                          @"Sid":[NSString stringWithUTF8String:sid?sid:"Sid"],
                          @"Role_Id":[NSString stringWithUTF8String:role_id?role_id:"Role_Id"],
                          @"Order_Id":[NSString stringWithUTF8String:orderid?orderid:"Order_Id"],
                          @"EXT":[NSString stringWithUTF8String:ext?ext:"EXT"],
                          }];
        }
        
        const char* getSDKVersion(){
            
            const char* version = [XDCore getSDKVersion].UTF8String;
            
            return strdup(version);
        }
        
        const char* getAccessToken(){
            
            const char* token = [XDCore getAccessToken].UTF8String;
            
            if(!token){
                
                return strdup("");
            }
            
            return strdup(token);
        }
        
        void hideGuest(){
            
            NSLog(@"隐藏游客");
            
            [XDCore hideGuest];
        }
        
        void hideQQ(){
            
            NSLog(@"隐藏QQ");

            [XDCore hideQQ];
        }
        
        void hideWeiChat(){
            
            NSLog(@"隐藏微信");

            [XDCore hideWX];
        }
        
        void showVC(){
            
            NSLog(@"显示VC");
            
            [XDCore showVC];
        }
        
        void setQQWeb(){
            
            NSLog(@"QQ Web");
            
            [XDCore setQQWeb];
        }
        
        void setWXWeb(){
            
            NSLog(@"微信Web");

            [XDCore setWXWeb];
        }
        
        void sdk_debug_msg(const char* msg){
            
            UIAlertView * debugAlter = [[UIAlertView alloc] initWithTitle:nil message:[NSString stringWithUTF8String:msg] delegate:nil cancelButtonTitle:nil otherButtonTitles:@"ok", nil];
            
            [debugAlter show];
        }
        
#if __cplusplus
    }
#endif


# pragma mark - XDSDKCallBack

    
/**
 初始化成功
 */
- (void)onInitSucceed{
    
    UnitySendMessage(self.gameObjectName.UTF8String, "OnInitSucceed", "");
}

/**
 初始化失败
 
 @param error_msg 错误信息
 */
- (void)onInitFailed:(nullable NSString*)error_msg{
    
    UnitySendMessage(self.gameObjectName.UTF8String, "OnInitFailed", error_msg.UTF8String);
}


/**
 登录成功
 
 @param access_token Token
 */
- (void)onLoginSucceed:(nonnull NSString*)access_token{
    
    UnitySendMessage(self.gameObjectName.UTF8String, "OnLoginSucceed", access_token.UTF8String);
}

/**
 登录取消
 */
- (void)onLoginCanceled{

    UnitySendMessage(self.gameObjectName.UTF8String, "OnLoginCanceled", "");
}


/**
 登录失败
 
 @param error_msg 错误信息
 */
- (void)onLoginFailed:(nullable NSString*)error_msg{
    
    UnitySendMessage(self.gameObjectName.UTF8String, "OnLoginFailed", error_msg.UTF8String);
}


/**
 游客账号升级成功
 */
- (void)onGuestBindSucceed:(nonnull NSString*)access_token{
    
    UnitySendMessage(self.gameObjectName.UTF8String, "OnGuestBindSucceed", access_token.UTF8String);
}

/**
 登出成功
 */
- (void)onLogoutSucceed{
    
    UnitySendMessage(self.gameObjectName.UTF8String, "OnLogoutSucceed", "");
}


/**
 支付完成
 */
- (void)onPayCompleted{
    
    UnitySendMessage(self.gameObjectName.UTF8String, "OnPayCompleted", "");
}

/**
 支付失败
 
 @param error_msg 错误信息
 */
- (void)onPayFailed:(nullable NSString*)error_msg{
    
    UnitySendMessage(self.gameObjectName.UTF8String, "OnPayFailed", error_msg.UTF8String);
}

/**
 支付取消
 */
- (void)onPayCanceled{
    
    UnitySendMessage(self.gameObjectName.UTF8String, "OnPayCanceled", "");
}


@end


