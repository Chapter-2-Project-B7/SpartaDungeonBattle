# [🌧 B-7조] "Sparta Dungeon Battle"
Chapter 2-2 프로그래밍 심화 팀과제

## 📋 목차
1. [팀원](#🧑‍🤝‍🧑-팀원)
2. [구현 기능](#📌-구현-기능)
3. [와이어 프레임](#🖼-와이어-프레임)
4. [시연 영상](#📹-시연-영상)
5. [C# 컨벤션](#c-컨벤션)
6. [Git 컨벤션](#git-컨벤션)

## 🧑‍🤝‍🧑 팀원
- 팀장 박관엽: 플레이어, 직업, 스킬, 레벨업, 스탯 변화
  
- 팀원 윤정빈: 몬스터, 퀘스트, 전리품, 포션, 데이터 저장
  
- 팀원 최유정: 아이템, 상점, 콘솔 유틸리티, 플레이어 생성
  
- 팀원 진강산: 게임매니저, 전투, 스테이지

[목차로 돌아가기](#📋-목차)

## 📌 구현 기능
- 플레이어
  - 플레이어 생성
  - 직업 선택
  - 스킬
  - 레벨업
  - 스탯 변화

- 몬스터
  - 랜덤 전리품

- 인벤토리
  - 아이템 장착
  - 포션 사용
  
- 전투
  - 치명타
  - 회피
  - 스테이지

- 상점
  - 아이템 구매

- 퀘스트
  - 퀘스트 진행 사항 추적
  - 몬스터 처치 퀘스트
  - 레벨업 퀘스트

- 콘솔 유틸리티
  - 텍스트 패딩
  - 텍스트 색상
  - 게임 헤더

[목차로 돌아가기](#📋-목차)

## 🖼 와이어 프레임
![와이어프레임](https://github.com/Chapter-2-Project-B7/SpartaDungeonBattle/assets/159543415/29cb1eb0-f942-4c82-ab8d-39de4ec37397)

[목차로 돌아가기](#📋-목차)

## 📹 시연 영상
(영상)

[목차로 돌아가기](#📋-목차)

## C# 컨벤션

### Framework
- .NET **8.0**

### Class
- [PascalCase](#casing), **명사**를 사용하세요.
  
  ```csharp
  public class ExampleClass
  {
    ...
  }
  ```

### Interface
- [PascalCase](#casing), 이름 앞에 대문자 "**I**"를 붙입니다.

  ```csharp
  public interface IKillable
  {
    ... 
  }
  
  public interface IDamageable<T>
  {
    ...
  }
  ```

### Method
- [PascalCase](#casing), **동사**로 시작하세요.
  
  ```csharp
  public void SetInitialPosition()
  {
    ...
  }

  public int GetDirection()
  {
    ...
  }
  ```
- `bool` 값을 반환하면, **질문**의 형태로 표현하는 것이 좋습니다.

  ```csharp
  public bool IsNewPosition()
  {
    ...
  }
  ```

### Variable
- public fields에 [PascalCase](#casing), private에 [camelCase](#casing), **명사**를 사용하세요.

  ```csharp
  public float MaxHealth;

  private int totalDamage;
  ```
- `bool` 값이면, 변수명 앞에 **동사**를 붙이세요.

  ```csharp
  private bool isDead;
  ```

### Enum
- [PascalCase](#casing), **단수 명사**를 사용하세요.

  ```csharp
  public enum WeaponType
  {
    Knife,
    Gun,
    RocketLauncher, 
    BFG
  }
  ```

### Comment
- 코드의 줄 끝이 아닌 **상단**에 작성합니다.
- 주석 태그(//)와 주석 텍스트 사이에 **공백**을 한 칸 삽입합니다.

  ```csharp
  // 주석 예시 (O)
  private void InitializeGame() // 주석 예시 (X)
  {
    ...
  } 
  ```

### Formatter
#### CSharpier
  - [CSharpier](https://csharpier.com/)는 C#을 위한 독단적인 코드 포맷터로 Roslyn을 사용하여 코드를 구문 분석하고, 자체 규칙을 사용하여 코드를 다시 출력한다.

#### CSharpier 포맷터를 사용하는 이유
  - 다른 포맷터에 비해 변환 속도가 빠르며, 뛰어난 성능을 발휘한다.
  - 코드를 더 읽기 쉽고, 일관되게 포맷팅해 주는 것을 목표로 하기 때문에 가독성과 유지보수성이 크게 향상된다.
  - 독단적인 포맷팅(opinionated formatting)을 채택함으로써 불필요한 논쟁을 줄이고 개발에 집중할 수 있다.

#### Visual Studio에서 CSharpier 확장 사용법
  - Visual Studio 메뉴 바 -> 확장 -> 확장 관리<br/>
  ![확장설치01](https://github.com/Chapter-2-Project-B7/SpartaDungeonBattle/assets/159543415/74f6a296-346f-4d0b-84de-3f23f034e0ff)

  - 검색창에 CSharpier 검색 후 설치<br/>
  ![확장설치02](https://github.com/Chapter-2-Project-B7/SpartaDungeonBattle/assets/159543415/470304c0-c34f-45cf-b0f9-0f334d135879)

  - Visual Studio 재실행 후, 메뉴 바 -> 도구 -> 옵션<br/>
  ![확장설치03](https://github.com/Chapter-2-Project-B7/SpartaDungeonBattle/assets/159543415/fc785f8f-5e81-4644-b199-6a8ed32d3e75)

  - (저장 시 자동 포맷팅) 왼쪽 메뉴에서 CSharpier 선택 후, CSharpier-Global -> Reformat with CSharpier on Save -> True로 변경<br/>
  ![확장설치04](https://github.com/Chapter-2-Project-B7/SpartaDungeonBattle/assets/159543415/69964ccd-d7dd-4cb9-9cde-2cc735d0aaab)

  - (자동 저장) 왼쪽 메뉴에서 환경 -> 문서 선택 후, 'Visual Studio가 배경에 있을 때 자동으로 파일을 저장합니다' 체크<br/>
  ![확장설치05](https://github.com/Chapter-2-Project-B7/SpartaDungeonBattle/assets/159543415/828d7d2e-1f29-40a1-b486-7225c4a00721)

#### 포맷팅 제외 설정
- 제외할 코드 라인 **상단**에 주석 처리
  ```
  // csharpier-ignore
  무시할 코드 라인
  ```

- 제외할 코드 블록 **상하단**에 주석 처리
  ```
  // csharpier-ignore-start
  무시할
  코드
  블록
  // csharpier-ignore-end
  ```


### Casing
- **camelCase**<br/>
  첫 글자는 소문자이며 공백이나 구두점 없이 문구를 작성하고 단어를 하나의 대문자로 구분<br/>
  `examplePlayerController`, `maxHealthPoints`

- **PascalCase**<br/>
  카멜 케이스의 변형으로 첫 글자를 대문자로 표시<br/>
  `ExamplePlayerController`, `MaxHealthPoints`

- **snake_case**<br/>
  단어 사이의 공백은 밑줄 문자로 대체<br/>
  `example_player_controller`, `max_health_points`

- **kebab-case**<br/>
  단어 사이의 공백은 대시로 대체<br/>
  `example-player-controller`, `max-health-points`

- **hungarian notation**<br/>
  변수 및 함수의 인자 이름 앞에 데이터 타입을 명시<br/>
  `int iCounter`, `string strPlayerName`

- **hungarian notation**은 오래된 규칙이며 **Unity** 개발에서는 일반적으로 사용되지 않습니다.<br/>

[C# 컨벤션으로 돌아가기](#c-컨벤션)

[목차로 돌아가기](#📋-목차)

## Git 컨벤션

### 커밋
- **커밋 단위**<br/>
  커밋은 가능한 **최소 단위**로 작성하며, 각 커밋에는 **한 가지** 기능의 변경 사항만 포함시키도록 합니다.<br/>

- **커밋 메시지 형식**
  ```
  // github desktop을 사용한다는 전제로 작성합니다.

  Summary: <type>: <subject>

  Description: <body>
  ```
  **subject**
  - type: 과 subject 사이에 **공백**을 삽입합니다.
  - 첫 글자는 **대문자**로 작성하며, 끝에 마침표를 작성하지 않습니다.
  - 영문 기준 **50글자 이하**로 작성합니다.
  - 과거형을 사용하지 않고 **명령문**을 사용합니다.

  **boby**
  - **긴 설명**이 필요한 경우에만 작성합니다.
  - 원활하고 빠른 의사소통을 위해 **한글**로 작성합니다.
  - **어떻게** 작성했는지가 아닌, **무엇**을 **왜** 작성했는지 작성합니다.
  - 한글 기준 한 줄당 **60자 내외**로 줄바꿈을 해줍니다.

  **type**
  
  | type     | 설명 |
  | -------- | --- |
  | Init     | 프로젝트 초기 생성 |
  | Docs     | 문서 수정 |
  | Feat     | 새로운 기능 추가 |
  | Fix      | 버그 수정 |
  | Test     | 테스트 코드 추가 |
  | Refactor | 코드 리펙토링 |
  | Perf     | 성능 개선 |
  | Style    | 기능 수정 없이 코드 스타일 변경 (코드 포맷팅, 세미콜론 누락 등) |
  | Rename   | 파일 혹은 폴더명을 수정만 한 경우 |
  | Remove   | 파일을 삭제만 한 경우 |
  | Chore    | 빌드 업무 수정, 패키지 매니저 수정 (gitignore 수정 등) |
  | Build    | 빌드 파일 수정 |
  | Ci       | CI 설정 파일 수정 |<br/>
<br/>

### 브랜치
- **분기 전략**: Github Flow<br/>
  
  ![분기전략01](https://github.com/Chapter-2-Project-B7/SpartaDungeonBattle/assets/159543415/66ec992a-a903-4160-83eb-a53125d54997)

- **브랜치 명명** <br/>
  ```
  생성자-범주-기능

  예시) JKS-feature-skill, JKS-bugfix-displayItemList
  ```
  - **생성자**: 브랜치 생성자의 이니셜
  - **범주**: 브랜치의 범주(예: feature, bugfix, refactor, perf)
  - **기능**: 구체적인 기능이나 작업을 설명

[목차로 돌아가기](#📋-목차)
