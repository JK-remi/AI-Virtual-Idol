# AI-Virtual-Idol

MediaPipe 기반 실시간 모션 추적과 Azure AI를 활용하여
사용자가 자신만의 AI 버추얼 캐릭터를 생성하고,
직접 대화하거나 버추얼 아이돌이 되어 실시간으로 소통할 수 있는 플랫폼

## 🎬 Demo

\-▶ Demo Video(https://youtu.be/N3hU5rFb31I?si=Ayiu-H0NHnc_HXUg)

## 목차

- [프로젝트 소개](#프로젝트-소개)
- [프로젝트 개요](#프로젝트-개요)
- [시스템 아키텍처](#시스템-아키텍처)
- [기술 스택](#기술-스택)
- [주요 기능](#주요-기능)
  - [1. 버추얼 캐릭터 커스터마이징](#1-버추얼-캐릭터-커스터마이징)
  - [2. 실시간 모션 추적 및 모션 매핑 (Motion Mapping)](#2-실시간-모션-추적-및-모션-매핑motion-mapping)
  - [3. AI Character Mode](#3-ai-character-mode)
  - [4. Virtual Idol Mode](#4-virtual-idol-mode)
- [기술적 문제 해결](#기술적-문제-해결)
  - [1. MediaPipe 랜드마크를 Unity Avatar Motion으로 변환](#1-mediapipe-랜드마크를-unity-avatar-motion으로-변환)
  - [2. System Prompt와 RAG를 활용한 캐릭터 일관성 유지](#2-system-prompt와-rag를-활용한-캐릭터-일관성-유지)
- [회고](#회고)

---

## 프로젝트 소개

AI-Virtual-Idol은 사용자가 자신만의 AI 버추얼 캐릭터를 생성하여 직접 대화하거나, 동일한 캐릭터가 되어 버추얼 아이돌처럼 팬들과 소통할 수 있는 플랫폼입니다.

MediaPipe 기반 실시간 모션 추적과 Azure OpenAI, Azure Speech(STT/TTS)를 활용하여 AI 캐릭터와의 자연스러운 상호작용을 구현했으며, Azure AI Search(RAG)와 Prompt Engineering을 통해 캐릭터의 성격과 세계관을 일관성 있게 유지하도록 설계했습니다.

### 주요 특징

- AI 캐릭터 생성 및 커스터마이징
- AI Character Mode를 통한 자연스러운 대화
- Virtual Idol Mode를 통한 실시간 방송
- MediaPipe 기반 실시간 모션 매핑
- Azure AI 기반 음성 및 LLM 연동

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

### 2. 실시간 모션 추적 및 모션 매핑 (Motion Mapping)

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

### 1. MediaPipe 랜드마크를 Unity Avatar Motion으로 변환

#### 문제

MediaPipe는 **Pose, Hands, Face** 랜드마크를 각각 독립적으로 제공하지만, Unity Avatar는 **Bone**과 **Blend Shape** 기반으로 동작하기 때문에 데이터를 그대로 적용할 수 없었습니다.

또한 MediaPipe와 Unity의 좌표계 차이로 인해 Avatar가 실제 움직임과 반대로 동작하거나, 손목 회전 및 일부 관절에서 잔떨림과 부자연스러운 움직임이 발생하는 문제가 있었습니다.

#### 해결

Pose, Hands, Face 데이터를 각각 독립적으로 처리하고 별도의 UDP 포트를 통해 Unity로 전달하여 Avatar의 각 구성 요소를 개별적으로 제어했습니다.

손목 회전은 손목, 검지, 중지 랜드마크를 이용하여 법선 벡터를 계산하고 `Quaternion.LookRotation()`을 통해 Bone Rotation으로 변환했습니다. 얼굴 표정은 MediaPipe가 제공하는 Blend Shape 값을 Unity의 **Skinned Mesh Renderer**와 매핑하여 Avatar의 표정에 적용했습니다.

```mermaid
flowchart LR

    Camera --> MediaPipe

    MediaPipe --> Pose
    MediaPipe --> Hands
    MediaPipe --> Face

    Pose -->|"UDP"| Unity
    Hands -->|"UDP"| Unity
    Face -->|"UDP"| Unity

    Unity --> Bone["Bone Mapping"]
    Unity --> Blend["Blend Shape Mapping"]

    Bone --> Avatar
    Blend --> Avatar
```

<p align="center">
    <img src="./images/mediapipe_landmark.png" width="48%">
    &nbsp;
    <img src="./images/motion_mapping.png" width="48%">
</p>

#### 결과

MediaPipe의 랜드마크를 Unity Avatar Motion으로 변환하여 실시간 모션 매핑과 얼굴 표정 표현을 구현했습니다.

다만 좌표계 차이와 Landmark 기반 추정의 한계로 인해 일부 관절에서 잔떨림과 방향 불일치가 발생했습니다. IK 적용과 회전 보정 등 다양한 방법으로 개선을 시도했지만 상용 서비스 수준의 자연스러운 Motion Quality에는 도달하지 못했으며, 이를 통해 실시간 Motion Mapping에서 좌표계와 Bone 구조를 함께 고려한 보정의 중요성을 확인할 수 있었습니다.

### 2. System Prompt와 RAG를 활용한 캐릭터 일관성 유지

#### 문제

이 프로젝트는 사용자가 직접 AI 캐릭터를 생성하여 대화하거나, 프로젝트에서 제공하는 버추얼 아이돌과 상호작용할 수 있도록 설계되었습니다. 따라서 캐릭터의 말투와 성격을 지속적으로 유지하는 것이 중요했습니다.

하지만 LLM은 동일한 질문에도 답변이 달라질 수 있으며, 대화가 길어질수록 캐릭터 설정에서 벗어난 응답을 생성하는 문제가 있었습니다.

#### 해결

사용자가 직접 캐릭터를 생성하는 경우에는 **Character Prompt**와 **System Policy**를 하나의 **System Prompt**로 구성하여 Azure OpenAI에 전달했습니다.

추가 설정이 필요한 경우에는 **txt 파일(최대 350KB)** 을 업로드할 수 있도록 하였으며, 업로드된 내용을 Grounding Data로 System Prompt에 포함하여 응답 생성 시 참고하도록 구현했습니다.

프로젝트에서 미리 제공하는 캐릭터를 사용하는 경우에는 **Azure AI Search(RAG)** 를 Data Source로 연결하여 캐릭터의 활동 정보와 설정을 검색하고, 검색된 문서를 Azure OpenAI가 함께 참조하여 응답을 생성하도록 구성했습니다.

```mermaid
flowchart TD

    Character["User-created Character"]
    Default["Default Character"]

    Character --> Prompt["System Prompt<br/>(Persona + Policy + Grounding)"]

    Default --> Search["Azure AI Search (RAG)"]

    Prompt --> GPT["Azure OpenAI"]
    Search --> GPT

    User["User Question"] --> GPT

    GPT --> Answer["Character Response"]
```

#### 결과

사용자가 생성한 캐릭터는 Character Prompt와 Grounding Data를 기반으로 원하는 성격과 말투를 유지하도록 구현했으며, 프로젝트에서 제공하는 캐릭터는 Azure AI Search를 통해 활동 정보와 설정을 지속적으로 참조하여 보다 일관된 응답을 생성할 수 있도록 구현했습니다.

이를 통해 캐릭터 생성 방식에 따라 Prompt 기반과 RAG 기반을 구분하여 적용하고, 다양한 캐릭터를 동일한 시스템에서 유연하게 활용할 수 있었습니다.

---

## 회고

### 프로젝트를 통해 배운 점

이번 프로젝트를 통해 **Unity와 AI 서비스를 하나의 애플리케이션으로 통합하는 방법**을 경험할 수 있었습니다. Azure OpenAI, Azure Speech, Azure AI Search를 연계하여 AI 캐릭터를 구현하는 과정에서 Prompt Engineering과 RAG의 활용 방식을 익혔으며, 캐릭터의 일관성을 유지하기 위한 System Prompt 설계의 중요성을 배울 수 있었습니다.

또한 MediaPipe와 Unity를 연동하면서 실시간 랜드마크 데이터를 Avatar의 Bone과 Blend Shape에 적용하는 과정을 경험하였고, 실시간 데이터 처리와 AI 서비스 연동의 차이를 고려하여 UDP와 REST API를 목적에 맞게 활용하는 방법을 익힐 수 있었습니다.

### 아쉬웠던 점

MediaPipe 기반 모션 매핑은 기본적인 움직임을 구현하는 데에는 성공했지만, 상용 서비스 수준의 자연스러운 품질에는 도달하지 못했습니다. 손목 회전이나 얼굴 표정은 비교적 안정적으로 구현되었지만, 일부 동작에서는 잔떨림(Jitter)과 부자연스러운 움직임이 발생했으며, 좌우 방향이 직관적으로 일치하지 않는 문제도 완전히 해결하지 못했습니다.

또한 AI 응답의 일관성을 높이기 위해 Prompt Engineering, Grounding Data, Azure AI Search(RAG)를 적용했지만, 캐릭터의 장기 기억이나 이전 대화 내용을 지속적으로 유지하는 기능까지는 구현하지 못한 점이 아쉬웠습니다.

### 앞으로 개선하고 싶은 점

향후에는 IK(Inverse Kinematics)와 모션 보간(Smoothing)을 적용하여 보다 자연스러운 Avatar 움직임을 구현하고, 얼굴 표정과 음성의 감정을 함께 반영하는 기능을 추가하고 싶습니다.

또한 Character Memory를 도입하여 이전 대화 내용을 기억하는 장기 대화를 지원하고, 캐릭터 생성 기능도 외형, 성격, 음성을 보다 자유롭게 커스터마이징할 수 있도록 확장하여 실제 버추얼 아이돌 제작 플랫폼에 가까운 형태로 발전시키고 싶습니다.

---

## Contact
- Email : 1abcm1@naver.com
- GitHub: https://github.com/JK-remi
