1. Android改用dialog实现，弹出直播窗口不再暂停游戏。

2. 新增 CloseXDLive() 接口

3. 新增 InvokeFunc(Dictionary<string, object> parameters, Action<Dictionary<string, object>> callback) 接口，可用来调取直播内提示


```
请按照如下内容，转换成 Dictionary 传入 parameters
 {
  type: 'alert',
  config: {
    id: '', //  透传参数
    // 左侧图片地址
    image: '',
    // 提示内容，//支持基本的 bbCode解析,如颜色。eg：您收到奥特曼的 [color=yellow]组队邀请[/color] 是否返回游戏查看？
    // 或 [color=#ffbf00]组队邀请[/color]
    content: '',
    // 按钮数组
    buttons: [
      {
        key: 'no', // 按钮点击时 回传该值
        content: '忽略',
        type: '', // 可选，default || primary 
      },
      {
        key: 'yes',
        content: '去查看',
        type: 'primary', // 可选，default || primary
      },
    ],
    checkbox: {
      content: '5分钟内不再提示',
      value: true, // 默认false,需要默认勾选时，传此值为true
    },
  },
}

回调结果仍为 Dictionary , 转换成json格式如下
 {
  id: '', //透传参数
  success :true,
  msg:'',// 错误信息
  payload: {
    checkbox: true,
    buttonKey: 'yes',
  },
}

示例
Dictionary<string, object> parameters = new Dictionary<string, object>();
parameters.Add("type", "alert");
parameters.Add("id", "0");
Dictionary<string, object> config = new Dictionary<string, object>();
config.Add("image", "/images/invitation.png");
config.Add("content", "您收到奥特曼的[size=16][color=#ffb100]私聊[/color][/size]是否返回游戏查看");
List<object> buttons = new List<object>();
Dictionary<string, object> no = new Dictionary<string, object>();
no.Add("key", "no");
no.Add("content", "忽略");
no.Add("type", "");
buttons.Add(no);
Dictionary<string, object> yes = new Dictionary<string, object>();
yes.Add("key", "yes");
yes.Add("content", "去查看");
yes.Add("type", "primary");
buttons.Add(yes);
config.Add("buttons", buttons);
Dictionary<string, object> checkbox = new Dictionary<string, object>();
checkbox.Add("content", "5分钟内不再提示");
checkbox.Add("value", true);
config.Add("checkbox", checkbox);
parameters.Add("config", config);
com.xdsdk.xdlive.XDLive.Instance.InvokeFunc(parameters, (params1)=>{
    com.xdsdk.xdlive.XDLive.Instance.CloseXDLive();
});

```