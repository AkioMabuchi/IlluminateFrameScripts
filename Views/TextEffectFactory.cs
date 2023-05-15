using Classes.Statics;
using Enums;
using Structs;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Views.Instances;

namespace Views
{
    public class TextEffectFactory : MonoBehaviour
    {
        [SerializeField] private TextEffect prefabTextEffect;
        
        [SerializeField] private Vector3 nextTilePositionSmall;
        [SerializeField] private Vector3 nextTilePositionMedium;
        [SerializeField] private Vector3 nextTilePositionLarge;
        
        [SerializeField] private Vector3 boardBasePositionSmall;
        [SerializeField] private Vector3 boardBasePositionMedium;
        [SerializeField] private Vector3 boardBasePositionLarge;
        
        private Vector3 _nextTilePosition = Vector3.zero;
        private Vector3 _boardBasePosition = Vector3.zero;
        
        public void SetNextTilePosition(FrameSize frameSize)
        {
            _nextTilePosition = frameSize switch
            {
                FrameSize.Small => nextTilePositionSmall,
                FrameSize.Medium => nextTilePositionMedium,
                FrameSize.Large => nextTilePositionLarge,
                _ => Vector3.zero
            };
        }
        public void SetBoardBasePosition(FrameSize frameSize)
        {
            _boardBasePosition = frameSize switch
            {
                FrameSize.Small => boardBasePositionSmall,
                FrameSize.Medium => boardBasePositionMedium,
                FrameSize.Large => boardBasePositionLarge,
                _ => Vector3.zero
            };
        }
    
        public void GenerateTextEffectLineScore(Vector2Int cellPosition, ElectricStatus electricStatus, int score)
        {
            var position = new Vector3(cellPosition.x * Const.TileSize, 0.0f, cellPosition.y * Const.TileSize)
                           + _boardBasePosition;

            Instantiate(prefabTextEffect, position, Quaternion.identity, transform).SetParams(new TextEffectParamsGroup
            {
                startPositionY = 0.1f,
                endPositionY = 0.3f,
                duration = 1.0f,
                materialType= electricStatus switch
                {
                    ElectricStatus.Normal =>TextEffectMaterialType.ElectricNormal,
                    ElectricStatus.Plus => TextEffectMaterialType.ElectricPlus,
                    ElectricStatus.Minus => TextEffectMaterialType.ElectricMinus,
                    ElectricStatus.Alternating => TextEffectMaterialType.ElectricAlternating,
                    _ =>TextEffectMaterialType.None
                },
                text = "<size=0.8>" + score + "</size><size=0.5>pts</size>"
            });
        }

        public void GenerateTextEffectLineCount(Vector2Int cellPosition, ElectricStatus electricStatus, int lineCount)
        {
            var position = new Vector3(cellPosition.x * Const.TileSize, 0.0f, cellPosition.y * Const.TileSize)
                           + _boardBasePosition;
            
            Instantiate(prefabTextEffect, position, Quaternion.identity, transform)
                .SetParams(new TextEffectParamsGroup
                {
                    startPositionY = 0.1f,
                    endPositionY = 0.2f,
                    duration = 0.8f,
                    materialType= electricStatus switch
                    {
                        ElectricStatus.Normal =>TextEffectMaterialType.ElectricNormal,
                        ElectricStatus.Plus => TextEffectMaterialType.ElectricPlus,
                        ElectricStatus.Minus => TextEffectMaterialType.ElectricMinus,
                        ElectricStatus.Alternating => TextEffectMaterialType.ElectricAlternating,
                        _ =>TextEffectMaterialType.None
                    },
                    text = "<size=0.6>" + lineCount + "</size>"
                });
        }

        public void GenerateTextEffectIlluminateScore(Vector2Int cellPosition, ElectricStatus electricStatus, int score,
            int lineCount)
        {
            var position = new Vector3(cellPosition.x * Const.TileSize, 0.0f, cellPosition.y * Const.TileSize)
                           + _boardBasePosition;

            Instantiate(prefabTextEffect, position, Quaternion.identity, transform).SetParams(new TextEffectParamsGroup
            {
                startPositionY = 0.1f,
                endPositionY = 0.5f,
                duration = 2.0f,
                materialType = electricStatus switch
                {
                    ElectricStatus.Normal => TextEffectMaterialType.ElectricNormal,
                    ElectricStatus.Plus => TextEffectMaterialType.ElectricPlus,
                    ElectricStatus.Minus => TextEffectMaterialType.ElectricMinus,
                    ElectricStatus.Alternating => TextEffectMaterialType.ElectricAlternating,
                    _ => TextEffectMaterialType.None
                },
                text = "<size=0.8>" + lineCount + "</size><size=0.5>Lines</size>\n" +
                       "<size=1.0>" + score + "</size><size=0.625>pts</size>"
            });
        }

        public void GenerateTextEffectIlluminateScoreCorrect(Vector2Int cellPosition, ElectricStatus electricStatus,
            int score, int lineCount)
        {
            var position = new Vector3(cellPosition.x * Const.TileSize, 0.0f, cellPosition.y * Const.TileSize)
                           + _boardBasePosition;

            Instantiate(prefabTextEffect, position, Quaternion.identity, transform).SetParams(new TextEffectParamsGroup
            {
                startPositionY = 0.1f,
                endPositionY = 0.5f,
                duration = 2.0f,
                materialType = electricStatus switch
                {
                    ElectricStatus.Normal => TextEffectMaterialType.ElectricNormal,
                    ElectricStatus.Plus => TextEffectMaterialType.ElectricPlus,
                    ElectricStatus.Minus => TextEffectMaterialType.ElectricMinus,
                    ElectricStatus.Alternating => TextEffectMaterialType.ElectricAlternating,
                    _ => TextEffectMaterialType.None
                },
                text = "<size=1.0>" + lineCount + "</size><size=0.625>Lines</size>\n" +
                       "<size=0.6>Correct Polarity</size>\n" +
                       "<size=1.2>" + score + "</size><size=0.0.75>pts</size>"
            });
        }

        public void GenerateTextEffectNoPlace()
        {
            Instantiate(prefabTextEffect, _nextTilePosition, Quaternion.identity, transform)
                .SetParams(new TextEffectParamsGroup
                {
                    startPositionY = 0.1f,
                    endPositionY = 0.5f,
                    duration = 2.0f,
                    materialType = TextEffectMaterialType.NoPlace,
                    text = "<size=0.8>" + Localize.LocaleString(LocaleKey.NoPlace) + "</size>"
                });
        }
    }
}
