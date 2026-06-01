Unity开发的RPG游戏。
+ 项目内容：
1. 事件系统：基于***发布-订阅模式***，实现了自定义的事件中心。系统具备高扩展性和**广播**功能，增加事件只需要对数据进行修改，同时将事件发布者和订阅者的行为**解耦**。
2. 任务系统：基于自定义的事件系统和**数据驱动**原则实现了与其它系统解耦的任务系统，将每个任务步骤拆分为条件(Condition)和效果(Effect)，依靠***工厂模式***为每个任务步骤生成其条件和效果，保证高拓展性；通过json文件编辑任务数据，在任务运行时(Runtime)中结合事件中心处理单个任务的逻辑，并通过任务管理器单例调度所有任务。
3. Buff系统：使用**JsonUtility**将json文本转化为Buff数据实例，设计基础的Buff效果接口，不同的Buff以不同方式实现接口，通过***工厂模式***生成它们，从而实现了一个易于扩展的面向数据的Buff系统。项目将Buff系统与背包物品、角色数据相结合，实现了使用物品后获得改变属性的持续Buff和瞬时Buff的功能。
4. 背包系统：实现了***MVC框架***下的背包系统，同时在数据驱动原则下将物品的配置数据和动态数据分离，在物品数据库中通过**SO**存储所有物品的配置数据，Controller只通过Model处理动态数据，由View自行决定配置数据在UI中的显示。
5. 对话系统：使用json文件编辑对话数据，在对话数据库中利用**JsonUtility**将json文本转化为数据实例，遵循数据驱动原则；确保对话系统和任务系统的联合工作与解耦，让任务系统负责任务推动，对话系统负责UI表现。
6. 战斗系统：将角色的持续性数据（如最大生命值）和当前属性（如当前生命值）分离，并将数据统一管理，便于其它系统对战斗属性的访问和修改；实现了战斗的**碰撞检测**以及玩家和敌人基础的攻击、受击和死亡动画反馈。
7. AI行为树：通过Unity6的behavior包实现了简单的**AI行为树**，包含敌人AI的范围检测、巡逻、追踪和攻击逻辑。
8. 输入管理：使用**InputSystem**实现玩家的移动、跳跃、互动，第三人称相机的晃动以及UI中的确认、取消等行为；通过输入管理器单例实现玩家角色行为表和UI行为表的切换并管理全局输入。
9. 相机管理：采用**Cinemachine**中的Freelook Camera实现第三人称摄像机。
+ 游戏截图

游戏场景，包含任务、属性UI
<img width="1920" height="1140" alt="屏幕截图 2026-05-11 103400" src="https://github.com/user-attachments/assets/59b93f2a-6a07-42ca-a2cf-c1f0a79abdb7" />

进行对话
<img width="1920" height="1140" alt="屏幕截图 2026-05-11 103443" src="https://github.com/user-attachments/assets/478b80e5-bf8f-4cd9-a81f-4a50bc3a9f16" /> 

通过NPC对话接收任务
<img width="1920" height="1140" alt="屏幕截图 2026-05-11 103514" src="https://github.com/user-attachments/assets/80199a39-09e0-4e92-8396-ceb75a63c29e" />

接受任务后UI改变
<img width="1920" height="1140" alt="屏幕截图 2026-05-11 103535" src="https://github.com/user-attachments/assets/1ab0e06b-c39e-4346-a7a0-12f7582f9322" />

拾取物品后完成任务步骤，推进任务UI
<img width="1920" height="1140" alt="屏幕截图 2026-05-11 103555" src="https://github.com/user-attachments/assets/f5d6c665-673f-4795-bbfd-8d8a433785f1" />

完成任务后在背包中获得奖励
<img width="1920" height="1140" alt="屏幕截图 2026-05-11 103612" src="https://github.com/user-attachments/assets/ef59f39e-2949-4ad2-b559-efc2f3826143" />

背包中显示物品名称以及描述
<img width="1920" height="1140" alt="屏幕截图 2026-05-11 103632" src="https://github.com/user-attachments/assets/8477a722-63da-4ef0-b3c4-678a2a060723" />

通过使用物品，可以获得增强Buff
<img width="1920" height="1140" alt="屏幕截图 2026-05-11 103650" src="https://github.com/user-attachments/assets/d45ad16b-16bd-49d7-bd3f-48c8c8ecf4dd" />
<img width="1920" height="1140" alt="屏幕截图 2026-05-11 103706" src="https://github.com/user-attachments/assets/52c17c8a-5d5c-451d-af01-5ae9e1bfbae0" />

使用物品后，获得Buff，改变角色属性
<img width="1920" height="1140" alt="屏幕截图 2026-05-11 103716" src="https://github.com/user-attachments/assets/6b5ca936-e9a7-4150-9e64-48bab6840ea6" />

战斗锁定 
<img width="761" height="478" alt="screenshots" src="https://github.com/user-attachments/assets/09cefb4a-acc4-45bf-9b00-b63e5b544269" />
