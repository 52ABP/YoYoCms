jQuery.extend(jQuery.validator.messages, {
  required: "必填项",
  remote: "您输入的已经存在",
  email: "请输入正确格式的电子邮件",
  url: "请输入合法的网址",
  date: "请输入合法的日期",
  dateISO: "请输入合法的日期 (ISO).",
  number: "请输入合法的数字",
  digits: "只能输入整数",
  creditcard: "请输入合法的卡号",
  equalTo: "您输入的内容不一致",
  accept: "请输入拥有合法后缀名的字符串", 
  maxlength: jQuery.validator.format("请输入 长度最长 {0} 位字符"),
  minlength: jQuery.validator.format("请输入 长度最少 {0} 位字符"),
  rangelength: jQuery.validator.format("请输入 {0} 和 {1} 位字符"),
  range: jQuery.validator.format("请输入一个介于 {0} 和 {1} 之间的值"),
  max: jQuery.validator.format("请输入一个最大为{0} 的值"),
  min: jQuery.validator.format("请输入一个最小为{0} 的值")
});