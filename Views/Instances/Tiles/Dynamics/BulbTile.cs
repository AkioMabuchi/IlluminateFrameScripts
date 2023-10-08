using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Interfaces.Tiles;
using ScriptableObjects;
using UnityEngine;

namespace Views.Instances.Tiles.Dynamics
{
    public class BulbTile : TileBase, IBulbTile, IMovableTile, IRenderableTile, IRotatableTile, IThrowableTile
    {
        private static readonly int _emissionControl = Shader.PropertyToID("_EmissionControl");
        private static readonly int _emissionHue = Shader.PropertyToID("_EmissionHue");
        private static readonly int _additionalEmissionControl = Shader.PropertyToID("_AdditionalEmissionControl");
        private static readonly int _additionalEmissionHue = Shader.PropertyToID("_AdditionalEmissionHue");
        
        [SerializeField] private Transform transformRotatable;
        
        [SerializeField] private MeshRenderer meshRendererBase;
        [SerializeField] private MeshRenderer meshRendererLine;
        [SerializeField] private MeshRenderer meshRendererFrame;
        [SerializeField] private MeshRenderer meshRendererBulbBase;
        [SerializeField] private MeshRenderer meshRendererBulb;

        [SerializeField] private Material materialBulb;
        
        [SerializeField] private ScriptableObjectElectricMaterials electricMaterialsLine;
        [SerializeField] private ElectricParams electricParams;
        [SerializeField] private float durationIlluminate;
        [SerializeField] private Ease easeIlluminate;
        
        private RotateStatus _rotateStatus;
        private ElectricStatus _electricStatusLine = ElectricStatus.None;
        private ElectricStatus _electricStatusBulb = ElectricStatus.None;
        
        private Sequence _sequenceRotate;
        private Sequence _sequenceIlluminate;
        
        private Coroutine _coroutineThrow;
        
        private float _currentRotationY;


        private void Start()
        {
            meshRendererBulb.material = new Material(materialBulb);
        }

        public void SetRotateStatus(RotateStatus rotateStatus)
        {
            _rotateStatus = rotateStatus;
        }

        public void SetElectricStatusLine(ElectricStatus electricStatus)
        {
            _electricStatusLine = electricStatus;
        }

        public void SetElectricStatusBulb(ElectricStatus electricStatus)
        {
            _electricStatusBulb = electricStatus;
        }

        public void Take()
        {
            meshRendererBase.enabled = true;
            meshRendererLine.enabled = true;
            meshRendererFrame.enabled = true;
            meshRendererBulbBase.enabled = true;
            meshRendererBulb.enabled = true;
        }

        public void Release()
        {
            meshRendererBase.enabled = false;
            meshRendererLine.enabled = false;
            meshRendererFrame.enabled = false;
            meshRendererBulbBase.enabled = false;
            meshRendererBulb.enabled = false;
        }

        public void Move(Vector3 position)
        {
            transform.position = position;
        }
        public void Render()
        {
            meshRendererLine.material = electricMaterialsLine.GetElectricMaterial(_electricStatusLine);
            meshRendererBulb.material.SetFloat(_emissionControl, _electricStatusBulb switch
            {
                ElectricStatus.Normal => 1.0f,
                ElectricStatus.Plus => 1.0f,
                ElectricStatus.Minus => 1.0f,
                ElectricStatus.Alternating => 1.0f,
                _ => 0.0f
            });
            
            meshRendererBulb.material.SetFloat(_emissionHue, electricParams.GetHue(_electricStatusBulb));
            meshRendererBulb.material.SetFloat(_additionalEmissionHue, electricParams.GetHue(_electricStatusBulb));
        }

        public void RenderIlluminate(LineDirection lineDirectionIn, LineDirection lineDirectionOut)
        {
            switch (_rotateStatus)
            {
                case RotateStatus.Rotate0:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Left:
                        {
                            meshRendererLine.material =
                                electricMaterialsLine.GetIlluminatedMaterial(_electricStatusLine);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate90:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Up:
                        {
                            meshRendererLine.material =
                                electricMaterialsLine.GetIlluminatedMaterial(_electricStatusLine);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate180:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Right:
                        {
                            meshRendererLine.material =
                                electricMaterialsLine.GetIlluminatedMaterial(_electricStatusLine);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate270:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Down:
                        {
                            meshRendererLine.material =
                                electricMaterialsLine.GetIlluminatedMaterial(_electricStatusLine);
                            break;
                        }
                    }

                    break;
                }
            }
        }

        public void RenderDarken(IEnumerable<ElectricStatus> electricStatuses)
        {
            var hashsetElectricStatuses = new HashSet<ElectricStatus>(electricStatuses);

            if (hashsetElectricStatuses.Contains(_electricStatusLine))
            {
                meshRendererLine.material = electricMaterialsLine.None;
            }

            if (hashsetElectricStatuses.Contains(_electricStatusBulb))
            {
                meshRendererBulb.material.SetFloat(_emissionControl, 0.0f);
            }
        }

        public void RenderShort(ShortedStatus shortedStatus, LineDirection lineDirectionIn,
            LineDirection lineDirectionOut)
        {
            switch (_rotateStatus)
            {
                case RotateStatus.Rotate0:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Left:
                        {
                            meshRendererLine.material =
                                electricMaterialsLine.GetShortedMaterial(shortedStatus);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate90:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Up:
                        {
                            meshRendererLine.material =
                                electricMaterialsLine.GetShortedMaterial(shortedStatus);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate180:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Right:
                        {
                            meshRendererLine.material =
                                electricMaterialsLine.GetShortedMaterial(shortedStatus);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate270:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Down:
                        {
                            meshRendererLine.material =
                                electricMaterialsLine.GetShortedMaterial(shortedStatus);
                            break;
                        }
                    }

                    break;
                }
            }

            meshRendererBulb.material.SetFloat(_emissionControl, 0.0f);
        }

        public void RenderReset()
        {
            meshRendererLine.material = electricMaterialsLine.None;
            
            meshRendererBulb.material.SetFloat(_emissionControl, 0.0f);
        }

        public void Rotate()
        {
            _sequenceRotate?.Kill();

            var targetRotationY = _rotateStatus switch
            {
                RotateStatus.Rotate0 => 0.0f,
                RotateStatus.Rotate90 => 90.0f,
                RotateStatus.Rotate180 => 180.0f,
                RotateStatus.Rotate270 => 270.0f,
                _ => throw new Exception()
            };

            while (_currentRotationY > targetRotationY)
            {
                targetRotationY += 360.0f;
            }

            _sequenceRotate = DOTween.Sequence()
                .Append(DOTween
                    .To(() => _currentRotationY, value => { _currentRotationY = value; }, targetRotationY, 0.2f)
                    .SetEase(Ease.OutCubic))
                .OnUpdate(() =>
                {
                    transformRotatable.localEulerAngles = new Vector3(0.0f, _currentRotationY, 0.0f);
                })
                .OnComplete(() =>
                {
                    _currentRotationY = _rotateStatus switch
                    {
                        RotateStatus.Rotate0 => 0.0f,
                        RotateStatus.Rotate90 => 90.0f,
                        RotateStatus.Rotate180 => 180.0f,
                        RotateStatus.Rotate270 => 270.0f,
                        _ => throw new Exception()
                    };

                    transformRotatable.localEulerAngles = new Vector3(0.0f, _currentRotationY, 0.0f);
                }).SetLink(gameObject);
        }

        public void RotateImmediate()
        {
            _sequenceRotate?.Kill();

            _currentRotationY = _rotateStatus switch
            {
                RotateStatus.Rotate0 => 0.0f,
                RotateStatus.Rotate90 => 90.0f,
                RotateStatus.Rotate180 => 180.0f,
                RotateStatus.Rotate270 => 270.0f,
                _ => throw new Exception()
            };

            transformRotatable.localEulerAngles = new Vector3(0.0f, _currentRotationY, 0.0f);
        }

        public void RotateReset()
        {
            _sequenceRotate?.Kill();
            _rotateStatus = RotateStatus.Rotate0;
            _currentRotationY = 0.0f;
            transformRotatable.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }

        public void Throw(Vector3 velocity, Vector3 angularVelocity)
        {
            IEnumerator Coroutine()
            {
                for (var loopLimit = 0; loopLimit < int.MaxValue; loopLimit++)
                {
                    var position = transform.position;
                    position += velocity * Time.deltaTime;
                    transform.position = position;

                    var eulerAngles = transform.eulerAngles;
                    eulerAngles += angularVelocity * Time.deltaTime;
                    transform.eulerAngles = eulerAngles;
                    
                    yield return null;
                }
            }

            _coroutineThrow = StartCoroutine(Coroutine());
        }

        public void ThrowReset()
        {
            if (_coroutineThrow != null)
            {
                StopCoroutine(_coroutineThrow);
            }
            
            transform.eulerAngles = Vector3.zero;
        }

        public void Illuminate()
        {
            _sequenceIlluminate?.Kill();
            _sequenceIlluminate = DOTween.Sequence()
                .Append(meshRendererBulb.material.DOFloat(0.0f, _additionalEmissionControl, durationIlluminate)
                    .From(1.0f)
                    .SetEase(easeIlluminate)
                ).SetLink(gameObject);
        }
    }
}
