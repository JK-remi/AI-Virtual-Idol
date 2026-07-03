# AI-Virtual-Idol

> MediaPipe 기반 실시간 모션 추적과 Azure AI를 결합하여 사용자와 상호작용 가능한 AI 버추얼 캐릭터 플랫폼

## 🎬 Demo

\-▶ Demo Video)(https://youtu.be/N3hU5rFb31I?si=Ayiu-H0NHnc_HXUg)

## 목차

- 프로젝트 소개
- 프로젝트 개요
- 시스템 아키텍처
- 기술 스택
- 주요 기능
  - 실시간 모션 추적 및 아바타 모션 매핑(Avatar Motion Mapping)
  - AI 음성 대화(STT/TTS)
  - Azure OpenAI 기반 AI 캐릭터
  - YouTube Live Chat 연동
  - 버추얼 캐릭터 생성 및 적용
- 기술적 문제 해결
  - MediaPipe Landmark를 Unity Humanoid Bone으로 모션 매핑(Motion Mapping)
  - BlendShape를 이용한 표정 구현
  - Azure AI 서비스 통합
- 프로젝트를 통해 배운 점
- 아쉬웠던 점
- 앞으로 개선하고 싶은 점

---

## 프로젝트 소개

(작성 예정)

---

## 프로젝트 개요

| 항목 | 내용 |
|------|------|
| 프로젝트명 | eruzA |
| 프로젝트 형태 | Microsoft AI School 팀 프로젝트 |
| 개발 기간 | 2024.09.26 ~ 2024.10.29 |
| 개발 인원 | 6명 |
| 역할 | Unity Window Application / MediaPipe / Motion Mapping / YouTube Live Chat / Azure AI Integration |

### 담당 역할

- Unity Window Application 구현
- MediaPipe 기반 실시간 모션 추적
- Unity Avatar Motion Mapping
- Azure Speech(STT/TTS) 연동
- Azure OpenAI 연동
- YouTube Live Chat 연동
- 프로젝트 기능 통합

---

## 시스템 아키텍처
```mermaid
flowchart LR

    Camera["Camera"]
    Microphone["Microphone"]
    YouTube["YouTube Live Chat API"]

    Python["Python + MediaPipe"]

    STT["Azure Speech (STT)"]
    LLM["Azure OpenAI"]
    TTS["Azure Speech (TTS)"]

    Unity["Unity"]

    Avatar["Avatar"]

    Camera --> Python
    Python --> Unity

    Microphone --> STT
    STT --> LLM
    STT --> TTS

    YouTube --> LLM

    LLM --> TTS

    TTS --> Unity

    Unity --> Avatar
```
AI-Virtual-Idol은 MediaPipe 기반 실시간 모션 추적, Azure AI 음성 처리, YouTube Live Chat 연동을 하나의 Unity 애플리케이션으로 통합하여 구성했습니다.

---

## 기술 스택

| Category | Technologies |
|----------|--------------|
| **Engine** | Unity |
| **Language** | C#, Python |
| **AI** | Azure OpenAI, Azure Speech(STT/TTS) |
| **Computer Vision** | MediaPipe |
| **API** | YouTube Live Chat API |
| **Tool** | VRoid Studio, Blender |
| **Version Control** | Git, GitHub |

MediaPipe를 이용한 실시간 모션 추적과 모션 매핑(Motion Mapping)을 중심으로, Azure AI(STT/TTS, OpenAI)와 YouTube Live Chat API를 연동하여 사용자와 상호작용 가능한 AI 버추얼 캐릭터 플랫폼을 구현했습니다.

---

## 주요 기능

### 1. 버추얼 캐릭터 커스터마이징

팀에서 **VRoid Studio**를 통해 제작한 캐릭터를 기반으로, 사용자는 원하는 캐릭터를 선택한 뒤 **Azure Speech**를 통해 생성한 음성과 **Character Prompt**를 설정하여 자신만의 버추얼 캐릭터를 구성할 수 있습니다.

```mermaid
flowchart LR

    VRoid["VRoid Studio<br/>(Pre-created Avatar)"]
    Voice["Azure Speech"]
    Prompt["Character Prompt"]

    VRoid --> Avatar["Virtual Avatar"]
    Voice --> Avatar
    Prompt --> Avatar

    Avatar --> Unity
```

<p align="center">
  <img src="./images/avatar_create.png" width="85%">
</p>

<p align="center">
  <img src="./images/avatar_setting.png" width="60%">
</p>

### 2. 실시간 모션 추적 및 모션 매핑(Motion Mapping)

MediaPipe를 활용하여 사용자의 전신, 손, 얼굴 랜드마크를 실시간으로 추출하고, Python에서 처리한 모션 데이터를 UDP 통신을 통해 Unity로 전달하여 Virtual Avatar에 적용합니다.

```mermaid
flowchart LR

    Camera --> MediaPipe
    MediaPipe --> Python
    Python -->|"UDP"| Unity
    Unity --> Avatar
```

<p align="center">
  <img src="./images/motion_mapping.png" width="75%">
</p>

### 3. AI Character Mode

사용자는 텍스트 또는 음성을 통해 AI 캐릭터와 대화할 수 있습니다. 음성 입력은 **Azure Speech(STT)**를 통해 텍스트로 변환되며, **Azure OpenAI**가 설정된 Character Prompt를 기반으로 캐릭터의 성격을 반영한 응답을 생성합니다. 생성된 응답은 채팅으로 출력되는 동시에 **Azure Speech(TTS)**를 통해 캐릭터의 음성으로도 제공됩니다.

```mermaid
flowchart LR

    Text["Text Input"]
    Voice["Voice Input"]

    STT["Azure Speech (STT)"]
    LLM["Azure OpenAI"]

    Chat["Chat"]
    TTS["Azure Speech (TTS)"]

    Avatar["Virtual Avatar"]

    Text --> LLM
    Voice --> STT
    STT --> LLM

    LLM --> Chat
    LLM --> TTS

    TTS --> Avatar
```

<p align="center">
  <img src="./images/ai_character_chat.png" width="75%">
</p>

### 4. Virtual Idol Mode

MediaPipe 기반 모션 매핑(Motion Mapping)을 통해 사용자의 전신, 손, 얼굴 움직임을 실시간으로 Virtual Avatar에 반영하여 실제 버추얼 아이돌처럼 자연스러운 방송을 진행할 수 있습니다. 또한 YouTube Live Chat과 연동하여 시청자의 채팅에 AI가 Character Prompt를 기반으로 응답하고, 생성된 응답을 캐릭터의 음성과 채팅으로 출력하여 실시간 상호작용을 제공합니다.

```mermaid
flowchart LR

    Camera --> MediaPipe
    MediaPipe --> Python

    Chat["YouTube Live Chat"] --> LLM["Azure OpenAI"]
    LLM --> TTS["Azure Speech (TTS)"]

    Python --> Unity
    TTS --> Unity

    Unity --> Avatar["Virtual Avatar"]
```

<p align="center">
  <img src="./images/youtube_live_chat.png" width="75%">
</p>

---

## 기술적 문제 해결

### 1. MediaPipe Landmark를 Unity Humanoid Bone으로 Motion Mapping

(작성 예정)

### 2. BlendShape를 이용한 표정 구현

(작성 예정)

### 3. Azure AI 서비스 통합

(작성 예정)

---

## 프로젝트를 통해 배운 점

(작성 예정)

---

## 아쉬웠던 점

(작성 예정)

---

## 앞으로 개선하고 싶은 점

(작성 예정)
